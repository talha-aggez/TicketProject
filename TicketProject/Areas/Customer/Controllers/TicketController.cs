using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketProject.DataAccess.IReposityory;
using TicketProject.Models;

namespace TicketProject.Areas.Admin.Controllers
{
    [Area("Customer")]
    public class TicketController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        public TicketController(IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
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
        public IActionResult Upsert(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                if(ticket.Id == 0)
                {
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
        public IActionResult GetAll()
        {
            var allObj = _unitofwork.Ticket.GetAll();
            return Json(new { data = allObj });
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
