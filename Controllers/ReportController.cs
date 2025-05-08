using HrManagement.Models;
using HrManagement.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HrManagement.Controllers
{
    public class ReportController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult EmployeeListTabulator()
        {
            var departments = _unitOfWork.Department.GetAll().ToList();
            ViewBag.Departments = departments;
            return View();
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var companies = _unitOfWork.Company.GetAll(); // Or use async if needed
            var result = companies.Select(c => new {
                id = c.ComId,
                compName = c.ComName
            });
            return Json(result);
        }

        [HttpGet]
        public IActionResult GetDepartmentsByCompany(Guid companyId)
        {
            var departments = _unitOfWork.Department.GetAll()
                                .Where(d => d.ComId == companyId)
                                .Select(d => new { id = d.DeptId, deptName = d.DeptName })
                                .ToList();
            return Json(departments);
        }


        //[HttpGet]
        //public async Task<IActionResult> GetEmployeeList(Guid? deptId)
        //{
        //    var employees = await _unitOfWork.Employee.GetAllWithDetailsAsync();

        //    if (deptId.HasValue)
        //        employees = employees.Where(e => e.DeptId == deptId).ToList();

        //    var data = employees.Select(e => new
        //    {
        //        e.EmpName,
        //        JoinDate = e.dtJoin.ToString("yyyy-MM-dd"),
        //        ServiceDays = (DateTime.Now - e.dtJoin).Days,
        //        Department = e.Department?.DeptName,
        //        Designation = e.Designation?.DesigName,
        //        Shift = e.Shift?.ShiftName
        //    });

        //    return Json(data);
        //}

        [HttpGet]
        public async Task<IActionResult> GetEmployeeList(Guid? companyId, Guid? deptId)
        {
            var employees = await _unitOfWork.Employee.GetAllWithDetailsAsync();

            if (companyId.HasValue)
                employees = employees.Where(e => e.ComId == companyId).ToList();

            if (deptId.HasValue)
                employees = employees.Where(e => e.DeptId == deptId).ToList();

            var result = employees.Select(e => new
            {
                empName = e.EmpName,
                joinDate = e.dtJoin.ToString("yyyy-MM-dd"),
                serviceDays = (DateTime.Now - e.dtJoin).Days,
                department = e.Department?.DeptName,
                designation = e.Designation?.DesigName,
                shift = e.Shift?.ShiftName
            });

            return Json(result);
        }


        //[HttpGet]
        //public async Task<IActionResult> GetAttendanceTabulatorReport(Guid companyId, Guid departmentId, DateTime fromDate, DateTime toDate)
        //{
        //    var result = await _unitOfWork.ReportRepository.GetAttendanceReportAsync(companyId, departmentId, fromDate, toDate);
        //    return Ok(result);
        //}
        public IActionResult AttendanceTabulator()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetAttendanceTabulator(Guid? companyId, Guid? deptId, DateTime? fromDate, DateTime? toDate)
        {
            var attendance = await _unitOfWork.Attendance.GetAllWithEmployeeAsync();

            if (fromDate.HasValue && toDate.HasValue)
                attendance = attendance.Where(a => a.dtDate >= fromDate && a.dtDate <= toDate).ToList();

            if (companyId.HasValue)
                attendance = attendance.Where(a => a.Employee.ComId == companyId).ToList();

            if (deptId.HasValue)
                attendance = attendance.Where(a => a.Employee.DeptId == deptId).ToList();

            var data = attendance.Select(a => new
            {
                empName = a.Employee.EmpName,
                attDate = a.dtDate.ToString("yyyy-MM-dd"),
                status = a.AttStatus,
                department = a.Employee.Department?.DeptName,
                designation = a.Employee.Designation?.DesigName,
                shift = a.Employee.Shift?.ShiftName
            });

            return Json(data);
        }



        //[HttpGet]
        //public async Task<IActionResult> GetAttendanceSummary(Guid? deptId, DateTime? fromDate, DateTime? toDate)
        //{
        //    var attendances = await _unitOfWork.Attendance.GetAllWithEmployeeAsync();

        //    if (fromDate.HasValue && toDate.HasValue)
        //        attendances = attendances.Where(a => a.dtDate >= fromDate && a.dtDate <= toDate).ToList();

        //    if (deptId.HasValue)
        //        attendances = attendances.Where(a => a.Employee.DeptId == deptId).ToList();

        //    var summary = attendances
        //        .GroupBy(a => a.Employee)
        //        .Select(g => new
        //        {
        //            EmployeeName = g.Key.EmpName,
        //            Present = g.Count(x => x.AttStatus == "P"),
        //            Absent = g.Count(x => x.AttStatus == "A"),
        //            Late = g.Count(x => x.AttStatus == "L")
        //        });

        //    return Json(summary);
        //}

        public IActionResult SalaryTabulator()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetSalaryTabulator(Guid? companyId, Guid? deptId, int? year, int? month, bool? isPaid)
        {
            var salaries = await _unitOfWork.Salary.GetAllWithEmployeeAsync();

            if (year.HasValue && month.HasValue)
                salaries = salaries.Where(s => s.dtYear == year && s.dtMonth == month).ToList();

            if (companyId.HasValue)
                salaries = salaries.Where(s => s.Employee.ComId == companyId).ToList();

            if (deptId.HasValue)
                salaries = salaries.Where(s => s.Employee.DeptId == deptId).ToList();

            if (isPaid.HasValue)
                salaries = salaries.Where(s => s.IsPaid == isPaid).ToList();

            var data = salaries.Select(s => new
            {
                empName = s.Employee.EmpName,
                gross = s.Gross,
                basic = s.Basic,
                hrent = s.Hrent,
                medical = s.Medical,
                absentAmount = s.AbsentAmount,
                payableAmount = s.PayableAmount,
                isPaid = s.IsPaid ? "Paid" : "Unpaid",
                department = s.Employee.Department?.DeptName,
                designation = s.Employee.Designation?.DesigName
            });

            return Json(data);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetSalarySummary(Guid? deptId, int? year, int? month, bool? isPaid = null)
        //{
        //    var salaries = await _unitOfWork.Salary.GetAllWithEmployeeAsync();

        //    if (year.HasValue && month.HasValue)
        //        salaries = salaries.Where(s => s.dtYear == year && s.dtMonth == month).ToList();

        //    if (deptId.HasValue)
        //        salaries = salaries.Where(s => s.Employee.DeptId == deptId).ToList();

        //    if (isPaid.HasValue)
        //        salaries = salaries.Where(s => s.IsPaid == isPaid).ToList();

        //    var data = salaries.Select(s => new
        //    {
        //        s.Employee.EmpName,
        //        s.Gross,
        //        s.Basic,
        //        s.Hrent,
        //        s.Medical,
        //        s.AbsentAmount,
        //        s.PayableAmount,
        //        IsPaid = s.IsPaid ? "Paid" : "Unpaid"
        //    });

        //    return Json(data);
        //}

    }
}
