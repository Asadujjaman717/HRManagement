using HrManagement.Models;
using HrManagement.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace HrManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(Guid? companyId)
        {
            var companies = await _unitOfWork.Company.GetAllAsync();
            var departments = await _unitOfWork.Department.GetAllAsync();
            var designations = await _unitOfWork.Designation.GetAllAsync();
            var shifts = await _unitOfWork.Shift.GetAllAsync();

            ViewBag.Companies = companies;
            ViewBag.Departments = departments;
            ViewBag.Designations = designations;
            ViewBag.Shifts = shifts;

            Guid selectedCompany = companyId ?? companies.FirstOrDefault()?.ComId ?? Guid.Empty;
            ViewBag.SelectedCompany = selectedCompany;

            var employees = await _unitOfWork.Employee.GetByCompanyIdAsync(selectedCompany);
            return View(employees);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetById(Guid id)
        //{
        //    var employee = await _unitOfWork.Employee.GetByIdAsync(id);
        //    return Json(employee);
        //}
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var employee = await _unitOfWork.Employee.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            return Json(employee);
        }


        [HttpGet]
        public async Task<IActionResult> GetDesignationsByDepartment(Guid companyId, Guid departmentId)
        {
            var designations = await _unitOfWork.Designation.GetAllAsync();
            var filtered = designations
                .Where(d => d.ComId == companyId && d.DeptId == departmentId)
                .Select(d => new
                {
                    desigId = d.DesigId,
                    desigName = d.DesigName
                }).ToList();

            return Json(filtered);
        }


        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    var errors = ModelState.Values
                //        .SelectMany(v => v.Errors)
                //        .Select(e => e.ErrorMessage)
                //        .ToList();
                //    return Json(new { success = false, message = "Invalid Data: " + string.Join(" | ", errors) });
                //}

                // Unique EmpCode Check
                var existEmp = await _unitOfWork.Employee.GetByEmpCodeAsync(employee.EmpCode);
                if (existEmp != null)
                {
                    return Json(new { success = false, message = "Employee Code must be unique." });
                }

                // Auto Calculate Salary parts
                // Auto Calculate Salary parts
                var company = await _unitOfWork.Company.GetByIdAsync(employee.ComId);
                if (company != null)
                {
                    employee.Basic = Math.Round(employee.Gross * company.Basic, 2);   // Round to 2 decimals
                    employee.HRent = Math.Round(employee.Gross * company.Hrent, 2);   // Round to 2 decimals
                    employee.Medical = Math.Round((double)(employee.Gross * company.Medical), 2); // Round to 2 decimals
                    employee.Others = Math.Round(employee.Gross - (employee.Basic + employee.HRent + employee.Medical), 2); // Round to 2 decimals
                    employee.dtJoin = employee.dtJoin.ToUniversalTime();
                }

                //var company = await _unitOfWork.Company.GetByIdAsync(employee.ComId);
                //if (company != null)
                //{
                //    employee.Basic = employee.Gross * company.Basic;
                //    employee.HRent = employee.Gross * company.Hrent;
                //    employee.Medical = (double)(employee.Gross * company.Medical);
                //    employee.Others = employee.Gross - (employee.Basic + employee.HRent + employee.Medical);
                //}

                employee.EmpId = Guid.NewGuid();
                await _unitOfWork.Employee.AddAsync(employee);
                await _unitOfWork.SaveAsync();
                TempData["success"] = "Employee created successfully.";
                return Json(new { success = true });
            }
            catch(Exception ex)
            {
                return Json(new { success = true, data = ex });
            }
          
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid Data" });
            }

            // Recalculate Salary on Edit too
            // Auto Calculate Salary parts
            try
            {
                var company = await _unitOfWork.Company.GetByIdAsync(employee.ComId);
                if (company != null)
                {
                    employee.Basic = Math.Round(employee.Gross * company.Basic, 2);   // Round to 2 decimals
                    employee.HRent = Math.Round(employee.Gross * company.Hrent, 2);   // Round to 2 decimals
                    employee.Medical = Math.Round((double)(employee.Gross * company.Medical), 2); // Round to 2 decimals
                    employee.Others = Math.Round(employee.Gross - (employee.Basic + employee.HRent + employee.Medical), 2); // Round to 2 decimals
                    employee.dtJoin = employee.dtJoin.ToUniversalTime();
                }

                _unitOfWork.Employee.Update(employee);
                await _unitOfWork.SaveAsync();
                TempData["success"] = "Employee Updated successfully.";
                return Json(new { success = true });
            }
            catch(Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            

            //var company = await _unitOfWork.Company.GetByIdAsync(employee.ComId);
            //if (company != null)
            //{
            //    employee.Basic = employee.Gross * company.Basic;
            //    employee.HRent = employee.Gross * company.Hrent;
            //    employee.Medical = (double)(employee.Gross * company.Medical);
            //    employee.Others = employee.Gross - (employee.Basic + employee.HRent + employee.Medical);
            //}

            
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var employee = await _unitOfWork.Employee.GetByIdAsync(id);
            if (employee == null)
                return Json(new { success = false });

            _unitOfWork.Employee.Remove(employee);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "Employee deleted successfully.";
            return Json(new { success = true });
        }
    }
}
