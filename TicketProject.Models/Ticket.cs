using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace TicketProject.Models
{
    public class Ticket
    {
        public string UserEmail { get; set; }
        public string EmployeeEmail { get; set; }
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

        public ApplicationUser PersonelTicket { get; set;  } // ticketla ilgilenen user....

        public string status { get; set; }
        public bool lockTicketForUser { get; set; }
        [NotMapped]
        public List<ApplicationUser> UserList { get; set; }
        //public ApplicationUser  TicketController { get; set; } // ticketı kontrol eden user.....
    }
}
