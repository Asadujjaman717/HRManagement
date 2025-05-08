using HrManagement.Data;
using HrManagement.Models;
using HrManagement.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HrManagement.Repository
{
    public class AttendanceSummaryRepository : GenericRepository<AttendanceSummary>, IAttendanceSummaryRepository
    {
        private readonly AppDbContext _context;

        public AttendanceSummaryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AttendanceSummary>> GetByCompanyAndPeriodAsync(Guid companyId, int year, int month)
        {
            return await _context.AttendanceSummaries
                .Include(x => x.Employee)
                .Where(x => x.ComId == companyId && x.dtYear == year && x.dtMonth == month)
                .ToListAsync();
        }
    }
}
