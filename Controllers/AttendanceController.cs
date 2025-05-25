using HrManagement.Models;
using HrManagement.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace HrManagement.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendanceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(Guid? companyId)
        {
            var companies = await _unitOfWork.Company.GetAllAsync();
            var employees = await _unitOfWork.Employee.GetAllAsync();

            ViewBag.Companies = companies;
            ViewBag.Employees = employees;

            Guid selectedCompany = companyId ?? companies.FirstOrDefault()?.ComId ?? Guid.Empty;
            ViewBag.SelectedCompany = selectedCompany;

            var attendances = await _unitOfWork.Attendance.GetByCompanyIdAsync(selectedCompany);

            return View(attendances);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var attendance = await _unitOfWork.Attendance.GetByIdAsync(id);
            return Json(attendance);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Attendance attendance)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Json(new { success = false, message = "Invalid Data" });
            //}

            // Business rule: If Absent, clear InTime and OutTime
            if (attendance.AttStatus == "A")
            {
                attendance.InTime = null;
                attendance.OutTime = null;
                //attendance.InTime = attendance.InTime.ToUniversalTime();
                //attendance.OutTime = attendance.OutTime.ToUniversalTime();

            }

            attendance.Id = Guid.NewGuid();
            //attendance.dtDate = attendance.dtDate.ToUniversalTime();
            //attendance.dtDate = attendance.dtDate;

            //attendance.InTime = attendance.InTime.ToUniversalTime();
            //attendance.OutTime = attendance.OutTime.ToUniversalTime();


            await _unitOfWork.Attendance.AddAsync(attendance);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "Attendance created successfully.";
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Attendance attendance)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid Data" });
            }

            // Business rule: If Absent, clear InTime and OutTime
            if (attendance.AttStatus == "A")
            {
                attendance.InTime = null;
                attendance.OutTime = null;
            }
            //attendance.dtDate = attendance.dtDate.ToUniversalTime();
            //attendance.dtDate = attendance.dtDate;

            _unitOfWork.Attendance.Update(attendance);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "Attendance updated successfully.";
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var attendance = await _unitOfWork.Attendance.GetByIdAsync(id);
            if (attendance == null)
            {
                return Json(new { success = false });
            }

            _unitOfWork.Attendance.Remove(attendance);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "Attendance deleted successfully.";
            return Json(new { success = true });
        }
    }
}
