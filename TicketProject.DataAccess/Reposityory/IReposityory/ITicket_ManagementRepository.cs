using System;
using System.Collections.Generic;
using System.Text;
using TicketProject.DataAccess.IReposityory;
using TicketProject.Models;

namespace TicketProject.DataAccess.Reposityory.IReposityory
{
    public interface ITicket_ManagementRepository : IRepository<Ticket_Management>
    {
        void Update(Ticket_Management ticketManagements);
    }
}
