using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicketProject.DataAccess.IReposityory;
using TicketProject.Models;

namespace TicketProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            Company company = new Company();
            if (id == null)
            {
                return View(company);
            }
            company = _unitofwork.Company.Get(id.GetValueOrDefault());
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.Id == 0)
                {
                    _unitofwork.Company.Add(company);
                }
                else
                {
                    _unitofwork.Company.Update(company);
                }
                _unitofwork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitofwork.Company.GetAll();
            return Json(new { data = allObj });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitofwork.Company.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = true, message = "Delete Succesfull" });
            }
            _unitofwork.Company.Remove(objFromDb);
            _unitofwork.Save();
            return Json(new { success = true, message = "Delete Succesfull" });
        }
        #endregion
    }
}

