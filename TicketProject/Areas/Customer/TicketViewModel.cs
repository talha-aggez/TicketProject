using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketProject.Models;

namespace TicketProject.Areas.Customer
{
    public class TicketViewModel
    {
        public IEnumerable<Ticket> Tickets { get; set; }
        public int Sayac { get; set; }
    }
}
