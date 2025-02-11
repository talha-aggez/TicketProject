﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketProject.DataAccess.Data;
using TicketProject.DataAccess.IReposityory;
using TicketProject.Models;

namespace TicketProject.DataAccess.Reposityory
{
    public class TicketRepository : Repository<Ticket> , ITicketRepository
    {
        private readonly ApplicationDbContext _db;
        public TicketRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(Ticket ticket)
        {
            var objFromDb = _db.Tickets.FirstOrDefault(s => s.Id == ticket.Id);
            if (objFromDb != null)
            {
                if (ticket.ImageUrl != null)
                {
                    objFromDb.ImageUrl = ticket.ImageUrl;
                }
                objFromDb.Title = ticket.Title;
                objFromDb.Text = ticket.Text;
                objFromDb.status = ticket.status;
                objFromDb.PersonelTicket = ticket.PersonelTicket;
                objFromDb.EmployeeEmail = ticket.EmployeeEmail;
               _db.SaveChanges();
            }
        }
    }
}
