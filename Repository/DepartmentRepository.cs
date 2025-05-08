using HrManagement.Models;
using HrManagement.Repository.IRepository;
using HrManagement.Repository;
using HrManagement.Data;

using Microsoft.EntityFrameworkCore;

namespace HrManagement.Repository
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetByCompanyId(Guid comId)
        {
            return await _context.Departments
                .Where(d => d.ComId == comId)
                .ToListAsync();
        }


        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.ToList();
        }

    }
}
