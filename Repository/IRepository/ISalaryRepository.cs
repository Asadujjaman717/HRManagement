using HrManagement.Models;

namespace HrManagement.Repository.IRepository
{
    public interface ISalaryRepository : IGenericRepository<Salary>
    {
        Task<IEnumerable<Salary>> GetByCompanyAndPeriodAsync(Guid companyId, int year, int month);
        Task<bool> MarkAsPaidAsync(Guid id);
        Task<IEnumerable<Salary>> GetAllWithEmployeeAsync();
    }
}
