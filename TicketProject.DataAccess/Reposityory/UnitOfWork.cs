using System;
using System.Collections.Generic;
using System.Text;
using TicketProject.DataAccess.Data;
using TicketProject.DataAccess.IReposityory;
using TicketProject.DataAccess.Reposityory.IReposityory;

namespace TicketProject.DataAccess.Reposityory
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Ticket = new TicketRepository(_db);
            Company = new CompanyRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            SP_Call = new SP_Call(_db);
        }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public ITicketRepository Ticket { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public ITicket_ManagementRepository Ticket_Management { get; private set; }

        public ISP_Call SP_Call { get; private set; }

        SP_Call IUnitOfWork.SP_Call => throw new NotImplementedException();

        public void Dispose()
        {
            _db.Dispose();
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
