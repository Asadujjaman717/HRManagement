using HrManagement.Data;
using HrManagement.Models;
using HrManagement.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HrManagement.Repository
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        private readonly AppDbContext _context;
        public CompanyRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _context.Companies.AnyAsync(c => c.ComName == name);
        }

        public IEnumerable<Company> GetAll()
        {
            return _context.Companies.ToList();
        }
    }
}
