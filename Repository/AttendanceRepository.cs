using HrManagement.Data;
using HrManagement.Models;
using HrManagement.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HrManagement.Repository
{
    public class AttendanceRepository : GenericRepository<Attendance>, IAttendanceRepository
    {
        private readonly AppDbContext _context;

        public AttendanceRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Attendance>> GetByCompanyIdAsync(Guid comId)
        {
            return await _context.Attendances
                .Include(a => a.Employee)
                .Where(a => a.ComId == comId)
                .OrderByDescending(a => a.dtDate)
                .ToListAsync();
        }
        //public async Task<List<Attendance>> GetAllWithEmployeeAsync()
        //{
        //    return await _context.Attendances
        //        .Include(a => a.Employee)
        //            .ThenInclude(e => e.Department)
        //        .Include(a => a.Employee)
        //            .ThenInclude(e => e.Designation)
        //        .Include(a => a.Employee)
        //            .ThenInclude(e => e.Shift)
        //        .ToListAsync();
        //}

        public async Task<IEnumerable<Attendance>> GetAllWithEmployeeAsync()
        {
            return await _context.Attendances
                .Include(a => a.Employee)
                    .ThenInclude(e => e.Department)
                .Include(a => a.Employee)
                    .ThenInclude(e => e.Designation)
                .Include(a => a.Employee)
                    .ThenInclude(e => e.Shift)
                .ToListAsync(); // still returns a List, but gets cast to IEnumerable
        }

        //public async Task<IEnumerable<Attendance>> GetAllWithEmployeeAsync()
        //{
        //    return await _context.Attendances
        //        .Include(a => a.Employee)
        //        .ToListAsync();
        //}
        //public async Task<List<AttendanceTabulatorDto>> GetAttendanceReportAsync(Guid companyId, Guid departmentId, DateTime fromDate, DateTime toDate)
        //{
        //    var query = from e in _context.Employees
        //                join d in _context.Departments on e.DeptId equals d.DeptId
        //                join a in _context.Attendances on e.EmpId equals a.EmpId
        //                where e.ComId == companyId && e.DeptId == departmentId
        //                      && a.dtDate >= fromDate && a.dtDate <= toDate
        //                group a by new { e.EmpName } into g
        //                select new AttendanceTabulatorDto
        //                {
        //                    EmpName = g.Key.EmpName,
        //                    Present = g.Count(x => x.AttStatus == "P"),
        //                    Absent = g.Count(x => x.AttStatus == "A"),
        //                    Late = g.Count(x => x.AttStatus == "L")
        //                };

        //    return await query.ToListAsync();
        //}

    }
}
