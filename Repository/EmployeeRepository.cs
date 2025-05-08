using HrManagement.Data;
using HrManagement.Models;
using HrManagement.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HrManagement.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllWithDetailsAsync()
        {
            return await _context.Employees
                .Include(e => e.Company)
                .Include(e => e.Department)
                .Include(e => e.Designation)
                .Include(e => e.Shift)
                .ToListAsync();
        }

        public override async Task<Employee?> GetByIdAsync(Guid id)
        {
            return await _context.Employees
                .Include(e => e.Company)
                .Include(e => e.Department)
                .Include(e => e.Designation)
                .Include(e => e.Shift)
                .FirstOrDefaultAsync(e => e.EmpId == id);
        }

        public async Task<IEnumerable<Employee>> GetByCompanyIdAsync(Guid comId)
        {
            return await _context.Employees
                .Include(e => e.Company)
                .Include(e => e.Shift)
                .Include(e => e.Department)
                .Include(e => e.Designation)
                .Where(e => e.ComId == comId)
                .ToListAsync();
        }

        public async Task<Employee?> GetByEmpCodeAsync(string empCode)
        {
            return await _context.Employees
                .Include(e => e.Company)
                .Include(e => e.Shift)
                .Include(e => e.Department)
                .Include(e => e.Designation)
                .FirstOrDefaultAsync(e => e.EmpCode == empCode);
        }
    }
}
