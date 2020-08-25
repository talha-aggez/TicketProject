using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketProject.Models;

namespace TicketProject.Areas.Admin
{
    public class DepartmentModel
    {
        public IEnumerable<Departments> Departments { get; set; }
        public int PageNumber { get; set; }
    }
}
