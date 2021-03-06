﻿using System.ComponentModel.DataAnnotations.Schema;

namespace OSTicketAPI.NET.Entities
{
    public partial class OstUserEmail
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Flags { get; set; }
        public string Address { get; set; }

        [ForeignKey("UserId")]
        public virtual OstUser OstUser { get; set; }
    }
}
