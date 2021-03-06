﻿using System;

namespace OSTicketAPI.NET.Entities
{
    public partial class OstTeam
    {
        public int TeamId { get; set; }
        public int LeadId { get; set; }
        public int Flags { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
