using HrManagement.Models;
using HrManagement.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace HrManagement.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var companies = await _unitOfWork.Company.GetAllAsync();
            return View(companies);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Company company)
        {
            Console.WriteLine("CREATE → IsInactive: " + company.IsInactive); // 🔍 LOG HERE
                                                                             // Manual validation for the sum
            var sum = company.Basic + company.Hrent + (company.Medical ?? 0);
            if (sum > 1)
            {
                return Json(new { success = false, message = "The sum of Basic, Hrent, and Medical must not exceed 1." });
            }

            company.ComId =  Guid.NewGuid();
            //if (!ModelState.IsValid)
            //{
            //    var errors = ModelState.Values
            //                    .SelectMany(v => v.Errors)
            //                    .Select(e => e.ErrorMessage)
            //                    .ToList();
            //    return Json(new { success = false, message = "Validation failed: " + string.Join(" | ", errors) });
            //}

            //company.ComId = Guid.NewGuid();
            await _unitOfWork.Company.AddAsync(company);
            await _unitOfWork.SaveAsync();
            return Json(new { success = true });
        }




        [HttpPost]
        public async Task<IActionResult> Edit(Company company)
        {
            Console.WriteLine("Edit → IsInactive: " + company.IsInactive); // 🔍 LOG HERE

            // Manual validation for the sum
            var sum = company.Basic + company.Hrent + (company.Medical ?? 0);
            if (sum > 1)
            {
                return Json(new { success = false, message = "The sum of Basic, Hrent, and Medical must not exceed 1." });
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Company.Update(company);
                await _unitOfWork.SaveAsync();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var company = await _unitOfWork.Company.GetByIdAsync(id);
            if (company == null) return Json(new { success = false });

            _unitOfWork.Company.Remove(company);
            await _unitOfWork.SaveAsync();
            return Json(new { success = true });
        }

        public async Task<IActionResult> GetById(Guid id)
        {
            var company = await _unitOfWork.Company.GetByIdAsync(id);
            return Json(company);
        }
    }
}
