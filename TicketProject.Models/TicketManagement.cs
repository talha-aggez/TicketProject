using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TicketProject.Models
{
    public class TicketManagement
    {
        [Key]
        public int Id { get; set; }
        public Ticket Ticket { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string status { get; set; }
    }
}
