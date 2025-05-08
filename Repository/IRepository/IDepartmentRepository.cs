using HrManagement.Models;
using HrManagement.Repository.IRepository;

namespace HrManagement.Repository.IRepository
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task<IEnumerable<Department>> GetByCompanyId(Guid comId);
        IEnumerable<Department> GetAll();

    }
}
