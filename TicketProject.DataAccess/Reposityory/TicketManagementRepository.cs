using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketProject.DataAccess.Data;
using TicketProject.DataAccess.Reposityory.IReposityory;
using TicketProject.Models;

namespace TicketProject.DataAccess.Reposityory
{
    public class TicketManagementRepository : Repository<Ticket_Management>, ITicket_ManagementRepository
    {
        private readonly ApplicationDbContext _db;
        public TicketManagementRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Ticket_Management ticket)
        {
            var objFromDb = _db.Ticket_Managements.FirstOrDefault(s => s.Ticket.Id == ticket.Ticket.Id);
            if (objFromDb != null)
            {
                objFromDb.Ticket = ticket.Ticket;
                objFromDb.Status = ticket.Status;
                _db.SaveChanges();
            }
        }
    }
}
