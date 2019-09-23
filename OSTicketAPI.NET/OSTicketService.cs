﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using OSTicketAPI.NET.DTO;
using OSTicketAPI.NET.Entities;
using OSTicketAPI.NET.Interfaces;
using OSTicketAPI.NET.Repositories;

namespace OSTicketAPI.NET
{
    public class OSTicketService
    {
        public IDepartmentRepository<OstDepartment> Departments { get; set; }
        public IFormRepository<OstForm> Forms { get; set; }
        public IHelpTopicRepository<OstHelpTopic> HelpTopics { get; set; }
        public IListRepository<OstList> Lists { get; set; }
        public ITicketRepository<OstTicket> Tickets { get; set; }
        public IUserRepository<OstUser> Users { get; set; }
        public IOSTicketOfficialApi OSTicketOfficialApi { get; }

        public OSTicketService(IOptions<OSTicketServiceOptions> options)
        {
            if (options.Value == null)
                throw new ArgumentException("OSTicketServiceOptions cannot be null", nameof(options));

            if (string.IsNullOrWhiteSpace(options.Value?.ConnectionString))
                throw new ArgumentException("Connection string cannot be null or empty", nameof(options.Value.ConnectionString));

            var osticketContext = BuildOSTicketContext(options.Value.ConnectionString);
            OSTicketOfficialApi = new OSTicketOfficialApi(options.Value.BaseUrl, options.Value.ApiKey);
            InitializeRepositories(osticketContext);
        }

        public OSTicketService(string databaseServer, string databaseUsername, string databasePassword, string databaseName, IOSTicketOfficialApi osTicketOfficialApi, int portNumber = 3306)
        {
            if (string.IsNullOrWhiteSpace(databaseServer))
                throw new ArgumentException("Database server cannot be null or empty", nameof(databaseServer));
            if (string.IsNullOrWhiteSpace(databaseUsername))
                throw new ArgumentException("Database username cannot be null or empty", nameof(databaseServer));
            if (string.IsNullOrWhiteSpace(databasePassword))
                throw new ArgumentException("Database password cannot be null or empty", nameof(databaseServer));
            if (string.IsNullOrWhiteSpace(databaseName))
                throw new ArgumentException("Database name cannot be null or empty", nameof(databaseServer));

            var builder = new MySqlConnectionStringBuilder
            {
                Server = databaseServer,
                UserID = databaseUsername,
                Password = databasePassword,
                Port = Convert.ToUInt32(portNumber)
            };
            var osticketContext = BuildOSTicketContext(builder.ToString());
            OSTicketOfficialApi = osTicketOfficialApi;
            InitializeRepositories(osticketContext);
        }

        public OSTicketService(string connectionString, IOSTicketOfficialApi osTicketOfficialApi)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string cannot be null or empty", nameof(connectionString));

            var osticketContext = BuildOSTicketContext(connectionString);
            OSTicketOfficialApi = osTicketOfficialApi;
            InitializeRepositories(osticketContext);
        }

        private void InitializeRepositories(OSTicketContext osticketContext)
        {
            Departments = new DepartmentRepository(osticketContext);
            Forms = new FormRepository(osticketContext);
            HelpTopics = new HelpTopicRepository(osticketContext);
            Lists = new ListRepository(osticketContext);
            Tickets = new TicketRepository(osticketContext);
            Users = new UserRepository(osticketContext);
        }

        private static OSTicketContext BuildOSTicketContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OSTicketContext>();
            var connectionStringBuilder = new MySqlConnectionStringBuilder(connectionString)
            {
                ConvertZeroDateTime = true,
                TreatTinyAsBoolean = false
            };
            optionsBuilder.UseMySql(connectionStringBuilder.ToString());
            return new OSTicketContext(optionsBuilder.Options);
        }
    }
}
