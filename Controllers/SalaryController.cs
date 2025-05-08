using HrManagement.Data;
using HrManagement.Models;
using HrManagement.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HrManagement.Controllers
{
    public class SalaryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public SalaryController(AppDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(Guid? companyId, int? year, int? month)
        {
            var companies = await _unitOfWork.Company.GetAllAsync();
            ViewBag.Companies = companies;

            Guid selectedCompany = companyId ?? companies.FirstOrDefault()?.ComId ?? Guid.Empty;
            int selectedYear = year ?? DateTime.Now.Year;
            int selectedMonth = month ?? DateTime.Now.Month;

            ViewBag.SelectedCompany = selectedCompany;
            ViewBag.SelectedYear = selectedYear;
            ViewBag.SelectedMonth = selectedMonth;

            List<Salary> salaries = new();

            if (companyId.HasValue && year.HasValue && month.HasValue)
            {
                // Call stored procedure to generate salary data
                //await _context.Database.ExecuteSqlRawAsync(
                //    "EXEC sp_CalculateSalary @p0, @p1, @p2",
                //    companyId, year, month
                //);

                await _context.Database.ExecuteSqlRawAsync(
                "SELECT generate_salary_summary({0}, {1}, {2})",
                companyId, year, month
            );

                // Load salaries from repository
                //salaries = (await _unitOfWork.Salary.GetByCompanyAndPeriodAsync(
                //    selectedCompany, selectedYear, selectedMonth)).ToList();

                salaries = (await _unitOfWork.Salary.GetByCompanyAndPeriodAsync(
                    selectedCompany, selectedYear, selectedMonth)).ToList();
            }

            return View(salaries);
        }

        [HttpPost]
        public async Task<IActionResult> MarkPaid(Guid id)
        {
            bool result = await _unitOfWork.Salary.MarkAsPaidAsync(id);
            return Json(new { success = result });
        }
    }
}
