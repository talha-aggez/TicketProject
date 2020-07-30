using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TicketProject.Models
{
    public class Ticket
    {
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
    }
}
