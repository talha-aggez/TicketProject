using System;
using System.Collections.Generic;
using System.Text;
using TicketProject.DataAccess.Reposityory;

namespace TicketProject.DataAccess.IReposityory
{
    public interface IUnitOfWork : IDisposable
    {
        ICompanyRepository Company { get; }
        ITicketRepository Ticket { get; }
        SP_Call SP_Call { get; }

        public void Save();
    }
}
