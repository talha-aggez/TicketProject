using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TicketProject.Utility;

namespace TicketProject.Models
{
    public class Ticket_Management
    {
        [Key]
        public int Id { get; set; }

        public Ticket Ticket { get; set; }

        public TicketStatus Status { get; set; }

        public DateTime Date { get; set; }
    }
}
