using HrManagement.Models;

namespace HrManagement.Repository.IRepository
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        // Removed duplicate method declaration to resolve ambiguity  
        Task<IEnumerable<Employee>> GetAllWithDetailsAsync();
        Task<IEnumerable<Employee>> GetByCompanyIdAsync(Guid comId);
        Task<Employee?> GetByEmpCodeAsync(string empCode);
    }
}
