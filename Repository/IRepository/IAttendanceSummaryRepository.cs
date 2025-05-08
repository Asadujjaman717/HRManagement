using HrManagement.Models;

namespace HrManagement.Repository.IRepository
{
    public interface IAttendanceSummaryRepository : IGenericRepository<AttendanceSummary>
    {
        Task<IEnumerable<AttendanceSummary>> GetByCompanyAndPeriodAsync(Guid companyId, int year, int month);
    }
}
