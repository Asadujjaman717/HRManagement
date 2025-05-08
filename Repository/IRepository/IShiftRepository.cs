using HrManagement.Models;

namespace HrManagement.Repository.IRepository
{
    public interface IShiftRepository : IGenericRepository<Shift>
    {
        Task<IEnumerable<Shift>> GetByCompanyIdAsync(Guid comId);
    }
}

