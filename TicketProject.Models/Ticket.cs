using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TicketProject.Models
{
    public class Ticket
    {
        public string UserEmail { get; set; }
        [Key] // Primary Key
        public int Id { get; set; } // ticket id...
        [Display(Name = "Ticket Title ")]
        [Required]
        [MaxLength(50)]
        [MinLength(10)]
        public string Title { get; set; } // ticket title...
        [Required]
        [MaxLength(50)]
        [MinLength(10)]
        public string Text { get; set; } // ticket text....
        public  ApplicationUser CreatingTicket { get; set; } // ticketı oluşturan user....
        //public ApplicationUser  TicketController { get; set; } // ticketı kontrol eden user.....
    }
}
