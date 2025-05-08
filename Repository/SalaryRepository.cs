using HrManagement.Data;
using HrManagement.Models;
using HrManagement.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HrManagement.Repository
{
    public class SalaryRepository : GenericRepository<Salary>, ISalaryRepository
    {
        private readonly AppDbContext _context;

        public SalaryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Salary>> GetByCompanyAndPeriodAsync(Guid companyId, int year, int month)
        {
            return await _context.Salaries
                .Include(x => x.Employee)
                .Where(x => x.ComId == companyId && x.dtYear == year && x.dtMonth == month)
                .ToListAsync();
        }

        public async Task<bool> MarkAsPaidAsync(Guid id)
        {
            var salary = await _context.Salaries.FindAsync(id);
            if (salary == null) return false;

            salary.IsPaid = true;
            salary.PaidAmount = salary.PayableAmount;
            _context.Salaries.Update(salary); // Removed 'await' as Update is a synchronous method
            await _context.SaveChangesAsync(); // SaveChangesAsync persists the changes asynchronously
            return true;
        }
        public async Task<IEnumerable<Salary>> GetAllWithEmployeeAsync()
        {
            return await _context.Salaries
                .Include(s => s.Employee)
                .Include(a => a.Employee)
                    .ThenInclude(e => e.Department)
                .Include(a => a.Employee)
                    .ThenInclude(e => e.Designation)
                .ToListAsync();
        }
    }
}
