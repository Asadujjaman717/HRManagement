

using HrManagement.Models;
using HrManagement.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace HrManagement.Controllers
{
    public class ShiftController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShiftController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(Guid? companyId)
        {
            var companies = await _unitOfWork.Company.GetAllAsync();
            ViewBag.Companies = companies;

            Guid selectedCompany = companyId ?? companies.FirstOrDefault()?.ComId ?? Guid.Empty;
            ViewBag.SelectedCompany = selectedCompany;

            var shifts = await _unitOfWork.Shift.GetByCompanyIdAsync(selectedCompany);
            return View(shifts);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var shift = await _unitOfWork.Shift.GetByIdAsync(id);
            return Json(shift);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Shift shift)
        {
            shift.ShiftId = Guid.NewGuid();
            await _unitOfWork.Shift.AddAsync(shift);
            await _unitOfWork.SaveAsync();
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Shift shift)
        {
            _unitOfWork.Shift.Update(shift);
            await _unitOfWork.SaveAsync();
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var shift = await _unitOfWork.Shift.GetByIdAsync(id);
            if (shift == null) return Json(new { success = false });
            _unitOfWork.Shift.Remove(shift);
            await _unitOfWork.SaveAsync();
            return Json(new { success = true });
        }
    }
}
