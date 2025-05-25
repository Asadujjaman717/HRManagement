using HrManagement.Models;
using HrManagement.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace HrManagement.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(Guid? companyId)
        {
            ViewBag.Companies = await _unitOfWork.Company.GetAllAsync();
            var selectedComId = companyId ?? Guid.Empty;
            ViewBag.SelectedCompany = selectedComId;

            var departments = selectedComId == Guid.Empty
                ? new List<Department>()
                : await _unitOfWork.Department.GetByCompanyId(selectedComId);

            return View(departments);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department dept)
        {
            //if (ModelState.IsValid)
            //{
            //    dept.DeptId = Guid.NewGuid();
            //    await _unitOfWork.Department.AddAsync(dept);
            //    await _unitOfWork.SaveAsync();
            //    return Json(new { success = true });
            //}
            //return Json(new { success = false, message = "Invalid Data" });
            dept.DeptId = Guid.NewGuid();
            await _unitOfWork.Department.AddAsync(dept);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "Department created successfully.";
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Department dept)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Department.Update(dept);
                await _unitOfWork.SaveAsync();
                TempData["success"] = "Department Edited successfully.";
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var dept = await _unitOfWork.Department.GetByIdAsync(id);
            if (dept == null) return Json(new { success = false });

            _unitOfWork.Department.Remove(dept);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "Department Deleted successfully.";
            return Json(new { success = true });
        }

        public async Task<IActionResult> GetById(Guid id)
        {
            var dept = await _unitOfWork.Department.GetByIdAsync(id);
            return Json(dept);
        }
    }
}
