using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketProject.DataAccess.Data;
using TicketProject.Models;
using TicketProject.Utility;

namespace TicketProject.Areas.Admin.Controllers
{
    public class Department : Controller
    {
        public string Username { get; private set; }
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _db;
        public Department(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            Username = userName;
        }
        public async Task<IActionResult> Index(int page = 1, int pageSize = 2)
        {
            var model = new DepartmentModel
            {
                PageNumber = page,
            };
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            await LoadAsync(user);
            model.Departments = await _db.Departments.Where(u => u.Status != RowStatus.Delete).Skip(((page - 1) * pageSize)).Take(pageSize).ToListAsync();
            return View(model);
        }
        public IActionResult Upsert(int? id)
        {
            Departments ticket = new Departments();
            if (id == null)
            {
                return View(ticket);
            }
            ticket = _db.Departments.Where(u => u.Id == id).FirstOrDefault();
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Departments ticket)
        {
            Departments department = new Departments();
            if (ModelState.IsValid)
            {
                if (ticket.Id == 0)
                {

                    department.Name = ticket.Name;
                    _db.Departments.Add(department);
                }
                else
                {
                    //_unitofwork.Ticket.Update(ticket);
                    //objFromDb.Title = ticket.Title;
                    //objFromDb.Text = ticket.Text;
                    //objFromDb.status = ticket.status;
                    //objFromDb.PersonelTicket = ticket.PersonelTicket;
                    //objFromDb.EmployeeEmail = ticket.EmployeeEmail;
                }
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }
    }
}
