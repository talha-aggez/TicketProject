using System;
using System.Collections.Generic;
using System.Text;
using TicketProject.Models;

namespace TicketProject.DataAccess.Reposityory.IReposityory
{
    public interface ITicketManagement
    {
        void Update(TicketManagement ticketManagement);
    }
}
