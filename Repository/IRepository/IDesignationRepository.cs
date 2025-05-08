using HrManagement.Models;
namespace HrManagement.Repository.IRepository
{
    public interface IDesignationRepository : IGenericRepository<Designation>
    {
        Task<IEnumerable<Designation>> GetByCompanyIdAsync(Guid comId);
    }
}
