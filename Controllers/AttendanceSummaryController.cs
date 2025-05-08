using HrManagement.Data;
using HrManagement.Models;
using HrManagement.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HrManagement.Controllers
{
    public class AttendanceSummaryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public AttendanceSummaryController(AppDbContext context, IUnitOfWork unitOfWork)
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

            List<AttendanceSummary> summaries = new();

            //if (companyId.HasValue && year.HasValue && month.HasValue)
            //{
            //    // Call stored procedure to generate attendance summary
            //    await _context.Database.ExecuteSqlRawAsync(
            //        "EXEC sp_GenerateAttendanceSummary @p0, @p1, @p2",
            //        companyId, year, month
            //    );

            //    // Load summaries from repository
            //    summaries = (await _unitOfWork.AttendanceSummary
            //        .GetByCompanyAndPeriodAsync(selectedCompany, selectedYear, selectedMonth)).ToList();
            //}
            if (companyId.HasValue && year.HasValue && month.HasValue)
            {
                // PostgreSQL-compatible function call
                await _context.Database.ExecuteSqlRawAsync(
                    "SELECT generate_attendance_summary({0}, {1}, {2});",
                    companyId.Value, year.Value, month.Value
                );

                summaries = (await _unitOfWork.AttendanceSummary
                    .GetByCompanyAndPeriodAsync(selectedCompany, selectedYear, selectedMonth)).ToList();
            }

            return View(summaries);
        }
    }
}
