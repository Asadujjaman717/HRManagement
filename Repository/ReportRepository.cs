//using Dapper;
//using HRM.Core.Interfaces;
//using HRM.Core.ViewModels;
//using Microsoft.Data.SqlClient;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace HRM.Infrastructure.Repositories
//{
//    public class ReportRepository : IReportRepository
//    {
//        private readonly ApplicationDbContext _context;

//        public ReportRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<List<AttendanceReportViewModel>> GetAttendanceReportAsync(Guid companyId, Guid? departmentId, DateTime fromDate, DateTime toDate)
//        {
//            var query = from emp in _context.Employees
//                        join att in _context.Attendances on emp.EmpId equals att.EmpId
//                        where emp.ComId == companyId &&
//                              (!departmentId.HasValue || emp.DeptId == departmentId) &&
//                              att.dtDate >= fromDate && att.dtDate <= toDate
//                        group att by new { emp.EmpName } into g
//                        select new AttendanceReportViewModel
//                        {
//                            EmployeeName = g.Key.EmpName,
//                            TotalPresent = g.Count(a => a.AttStatus == "P"),
//                            TotalLate = g.Count(a => a.AttStatus == "L"),
//                            TotalAbsent = g.Count(a => a.AttStatus == "A")
//                        };

//            return await query.ToListAsync();
//        }

//        public async Task<List<SalaryReportViewModel>> GetSalaryReportAsync(Guid companyId, Guid? departmentId, int year, int month, string? paymentStatus)
//        {
//            var query = from sal in _context.Salaries
//                        join emp in _context.Employees on sal.EmpId equals emp.EmpId
//                        where sal.ComId == companyId &&
//                              sal.dtYear == year && sal.dtMonth == month &&
//                              (!departmentId.HasValue || emp.DeptId == departmentId) &&
//                              (string.IsNullOrEmpty(paymentStatus) ||
//                                (paymentStatus == "Paid" && sal.IsPaid) ||
//                                (paymentStatus == "Unpaid" && !sal.IsPaid))
//                        select new SalaryReportViewModel
//                        {
//                            EmployeeName = emp.EmpName,
//                            Gross = sal.Gross,
//                            Basic = sal.Basic,
//                            HRent = sal.Hrent,
//                            Medical = sal.Medical,
//                            AbsentAmount = sal.AbsentAmount,
//                            PayableAmount = sal.PayableAmount
//                        };

//            return await query.ToListAsync();
//        }
//    }
//}
