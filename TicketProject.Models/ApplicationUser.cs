using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TicketProject.Models
{
    public class ApplicationUser : IdentityUser
    {
      [Required]
      public string FirstName { get; set; }
      [Required]
      public string LastName { get; set; }
      [NotMapped]
      public string Role { get; set; }

      public int? CompanyId { get; set; }
      [ForeignKey("CompanyId")]
      public Company Company { get; set; }
    }
}
