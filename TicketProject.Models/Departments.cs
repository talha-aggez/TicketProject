using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TicketProject.Utility;

namespace TicketProject.Models
{
    public class Departments
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public RowStatus Status { get; set; }
    }
}
