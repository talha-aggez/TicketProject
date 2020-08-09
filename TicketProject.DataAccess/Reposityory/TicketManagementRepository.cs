using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketProject.DataAccess.Data;
using TicketProject.DataAccess.Reposityory.IReposityory;
using TicketProject.Models;

namespace TicketProject.DataAccess.Reposityory
{
    public class TicketManagementRepository : Repository<TicketManagement> ,ITicketManagement
    {
        private readonly ApplicationDbContext _db;
        public TicketManagementRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(TicketManagement ticketManagement)
        {
            var objFromDb = _db.TicketManagements.FirstOrDefault();
            if (objFromDb != null)
            {
                /*objFromDb.Name = company.Name;
                objFromDb.StreetAddress = company.StreetAddress;
                objFromDb.City = company.City;
                objFromDb.State = company.State;
                objFromDb.PostalCode = company.PostalCode;
                objFromDb.PhoneNumber = company.PhoneNumber;
                _db.SaveChanges();*/
            }
        }
    }
}
