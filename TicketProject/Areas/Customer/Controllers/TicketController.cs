using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TicketProject.Areas.Customer;
using TicketProject.DataAccess.Data;
using TicketProject.DataAccess.IReposityory;
using TicketProject.Models;
using TicketProject.Utility;

namespace TicketProject.Areas.Admin.Controllers
{
    [Area("Customer")]
    public class TicketController : Controller
    {
        public string Username { get; private set; }

        private readonly IUnitOfWork _unitofwork;
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _hostEnvoirement;//resimleri dosyada güncelleme işlemi
        public IEnumerable<SelectListItem> SupportUserList { get; set; }
        public TicketController(IUnitOfWork unitOfWork,ApplicationDbContext db, UserManager<IdentityUser> userManager,IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _unitofwork = unitOfWork;
            _hostEnvoirement = hostEnvironment;
            _db = db;
        }
        public async Task<IActionResult> Index(int page = 1, int pageSize = 2)
        {
            var model = new TicketViewModel
            {
                PageNumber = page,
                Tickets = null,
            };
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            //var roles = await _userManager.GetRolesAsync(user);
            await LoadAsync(user);
            if (User.IsInRole(SD.Role_Admin))
            {
                //var allObj = _db.Tickets.Take(2).ToList();
                model.Tickets = await _db.Tickets.Where(u => u.status != RowStatus.Delete).Skip(((page - 1) * pageSize)).Take(pageSize).ToListAsync();
                return View(model);
            }
            if (User.IsInRole(SD.Role_SupportUsers))
            {
                model.Tickets = await _db.Tickets.Where(u => u.status != RowStatus.Delete && u.PersonelTicket != null).Skip(((page - 1) * pageSize)).Take(pageSize).ToListAsync();
                return View(model);
            }
            else if (User.IsInRole(SD.Role_CompanyAgents)){
                ApplicationUser temp = new ApplicationUser();
                temp = _db.ApplicationUsers.Where(u => u.UserName == user.UserName).FirstOrDefault();
                model.Tickets = await _db.Tickets.OrderByDescending(s => s.Id ).Where(u => u.PersonelTicket == temp && u.status != RowStatus.Delete).Skip(((page - 1) * pageSize)).Take(pageSize).ToListAsync();
                return View(model);
            }
            else
            {
                model.Tickets = await _db.Tickets.OrderByDescending(s=> s.Id).Where(u => u.UserEmail == Username && u.status != RowStatus.Delete).Skip(((page - 1) * pageSize)).Take(pageSize).ToListAsync();
                return View(model);
            }
        }
        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            Username = userName;
        }
        private IEnumerable<SelectListItem> GetUser()
        {
            var users = _unitofwork.ApplicationUser.GetAll().Select(x=> x.Email).Select(i => new SelectListItem
            {
                Text = i,
                Value = i,
            });
            return new SelectList(users, "Text", "Value");
        }
        public IActionResult UserAdd(int? id)
        {
            var model = new UserRoleViewModel
            {
                UserList = GetUser(),
                ticket = new Ticket(),
            };
            if (id == null)
            {
                return View(model);
            }
            model = new UserRoleViewModel
            {
                UserList = GetUser(),
                ticket = _unitofwork.Ticket.Get(id.GetValueOrDefault()),
            };
            if (model.ticket == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult UserAdd(UserRoleViewModel userRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                Ticket_Management ticket_Management = new Ticket_Management();
                string a = userRoleViewModel.ticket.EmployeeEmail + " ";
                int b = userRoleViewModel.ticket.Id;
                userRoleViewModel.ticket.PersonelTicket = _db.ApplicationUsers.Where(x => x.Email == userRoleViewModel.ticket.EmployeeEmail).FirstOrDefault();
                ticket_Management = _db.Ticket_Managements.Include(a => a.Ticket).Where(u => u.Ticket.Id == userRoleViewModel.ticket.Id).FirstOrDefault();
                ticket_Management.Status = TicketStatus.Seen;
                _unitofwork.Ticket.Update(userRoleViewModel.ticket);
                _unitofwork.Save();
              return RedirectToAction(nameof(Index));
            }
            return View(userRoleViewModel);
        }
        public  IActionResult Upsert(int? id)
        {
            Ticket ticket = new Ticket();
            if(id == null)
            {
                return View(ticket);
            }
            ticket = _unitofwork.Ticket.Get(id.GetValueOrDefault());
            if(ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Ticket ticket)
        {
            Ticket_Management ticket_Management = new Ticket_Management();
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvoirement.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images");
                    var extension = Path.GetExtension(files[0].FileName);
                    if (ticket.ImageUrl != null)
                    {
                        //remove old image or edit
                        var imagePath = Path.Combine(webRootPath, ticket.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    ticket.ImageUrl = @"\images\" + fileName + extension;
                }
                else
                {
                    //update when do not change 
                    if (ticket.Id != 0)
                    {
                        Ticket objFromDb = _unitofwork.Ticket.Get(ticket.Id);
                        ticket.ImageUrl = objFromDb.ImageUrl;
                    }
                }
                if (ticket.Id == 0)
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user == null)
                    {
                        return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                    }
                    await LoadAsync(user);
                    ticket.CreatingTicket = _db.ApplicationUsers.Where(u => u.Email == Username).FirstOrDefault();
                    ticket.UserEmail = ticket.CreatingTicket.Email;
                    ticket_Management.Ticket = ticket;
                    ticket_Management.Date = Date.Now;
                    ticket_Management.Status = TicketStatus.Sended;
                    _db.Ticket_Managements.Add(ticket_Management);
                    _unitofwork.Ticket.Add(ticket);
                }
                else
                {
                    _unitofwork.Ticket.Update(ticket);
                }
                _unitofwork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var objFromDb = await _db.Tickets.FindAsync(id);
            objFromDb.status = RowStatus.Delete;
            if (objFromDb == null)
            {
                return NotFound();
            }
            _unitofwork.Ticket.Update(objFromDb);
            _unitofwork.Save();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost , ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var objFromDb = await _db.Tickets.FindAsync(id);
            objFromDb.status = RowStatus.Delete;
            if(objFromDb == null)
            {
                return NotFound();
            }
            _unitofwork.Ticket.Update(objFromDb);
            _unitofwork.Save();
            return Redirect(nameof(Index));
        }
        public IActionResult Status(int? id)
        {
            Ticket_Management ticket = new Ticket_Management();
            if (id == null)
            {
                return View(ticket);
            }
            ticket = _db.Ticket_Managements.Where(u => u.Ticket.Id == id).FirstOrDefault();
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

    }
}
