using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
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
        public TicketController(IUnitOfWork unitOfWork,ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _unitofwork = unitOfWork;
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            Username = userName;
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
            if (ModelState.IsValid)
            {
                if(ticket.Id == 0)
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user == null)
                    {
                        return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                    }
                    await LoadAsync(user);
                    ticket.CreatingTicket = _db.ApplicationUsers.Where(u => u.Email == Username).FirstOrDefault();
                    ticket.UserEmail = ticket.CreatingTicket.Email;
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
        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            //var roles = await _userManager.GetRolesAsync(user);
            await LoadAsync(user);
            if (User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_CompanyAgents))
            {
                var allObj = _db.Tickets.ToList();
                return Json(new { data = allObj });
            }
            else
            {
                var allObj = _db.Tickets.Where(u=>u.UserEmail == Username).ToList();
                return Json(new { data = allObj });
            }
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitofwork.Ticket.Get(id);
            if(objFromDb == null)
            {
                return Json(new { success = true, message = "Delete Succesfull" });
            }
            _unitofwork.Ticket.Remove(objFromDb);
            _unitofwork.Save();
            return Json(new { success = true, message = "Delete Succesfull" });
        }
        #endregion
    }
}
