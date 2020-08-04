using System;
using System.Collections.Generic;
using System.Text;
using TicketProject.DataAccess.Reposityory;
using TicketProject.DataAccess.Reposityory.IReposityory;

namespace TicketProject.DataAccess.IReposityory
{
    public interface IUnitOfWork : IDisposable
    {
        ICompanyRepository Company { get; }
        ITicketRepository Ticket { get; }
        SP_Call SP_Call { get; }
        IApplicationUserRepository ApplicationUser{get;}
        public void Save();
    }
}
