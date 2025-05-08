using HrManagement.Models;

namespace HrManagement.Repository.IRepository
{
    public interface IAttendanceRepository : IGenericRepository<Attendance>
    {
        Task<IEnumerable<Attendance>> GetByCompanyIdAsync(Guid comId);
        Task<IEnumerable<Attendance>> GetAllWithEmployeeAsync();
    }
}
