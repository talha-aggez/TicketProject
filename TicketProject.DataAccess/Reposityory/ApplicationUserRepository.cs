using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketProject.DataAccess.Data;
using TicketProject.DataAccess.IReposityory;
using TicketProject.DataAccess.Reposityory.IReposityory;
using TicketProject.Models;

namespace TicketProject.DataAccess.Reposityory
{
    public class ApplicationUserRepository : Repository<ApplicationUser> , IApplicationUserRepository    {
        private readonly ApplicationDbContext _db;
        public  ApplicationUserRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }
    }
}
