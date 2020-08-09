using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketProject.Models;

namespace TicketProject.Areas.Customer
{
    public class UserRoleViewModel
    {
        public Ticket ticket { get; set; }
        public IEnumerable<SelectListItem> UserList { get; set; }
    }
}
