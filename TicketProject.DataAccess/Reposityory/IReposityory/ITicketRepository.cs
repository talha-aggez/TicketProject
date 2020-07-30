using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TicketProject.Models;

namespace TicketProject.DataAccess.IReposityory
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        void Update(Ticket ticket);
    }
}
