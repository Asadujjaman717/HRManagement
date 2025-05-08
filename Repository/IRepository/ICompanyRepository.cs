
using HrManagement.Models;
using HrManagement.Repository.IRepository;

namespace HrManagement.Repository.IRepository
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        Task<bool> ExistsAsync(string name);

        IEnumerable<Company> GetAll();
    }
}