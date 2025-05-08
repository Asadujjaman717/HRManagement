using HrManagement.Models;
using HrManagement.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace HrManagement.Controllers
{
    public class DesignationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DesignationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(Guid? companyId)
        {
            var companies = await _unitOfWork.Company.GetAllAsync();
            var departments = await _unitOfWork.Department.GetAllAsync();

            ViewBag.Companies = companies;
            ViewBag.Departments = departments;

            Guid selectedCompany = companyId ?? companies.FirstOrDefault()?.ComId ?? Guid.Empty;
            ViewBag.SelectedCompany = selectedCompany;

            var designations = await _unitOfWork.Designation.GetByCompanyIdAsync(selectedCompany);

            return View(designations);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var designation = await _unitOfWork.Designation.GetByIdAsync(id);
            return Json(designation);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Designation designation)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Json(new { success = false, message = "Invalid Data" });
            //}

            designation.DesigId = Guid.NewGuid();
            await _unitOfWork.Designation.AddAsync(designation);
            await _unitOfWork.SaveAsync();
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Designation designation)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Json(new { success = false, message = "Invalid Data" });
            //}

            _unitOfWork.Designation.Update(designation);
            await _unitOfWork.SaveAsync();
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var designation = await _unitOfWork.Designation.GetByIdAsync(id);
            if (designation == null)
            {
                return Json(new { success = false });
            }

            _unitOfWork.Designation.Remove(designation);
            await _unitOfWork.SaveAsync();
            return Json(new { success = true });
        }
    }
}
