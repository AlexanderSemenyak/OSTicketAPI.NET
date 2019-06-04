﻿using System.Threading.Tasks;
using OSTicketAPI.NET.Entities;

namespace OSTicketAPI.NET.Interfaces
{
    public interface IUserRepository
    {
        Task<OstUser> GetUserById(int id);
        Task<OstUser> GetUserByEmail(string email);
        Task<OstUser> GetUserByUsername(string username);
    }
}
