using HrManagement.Models;
using HrManagement.Repository.IRepository;
using HrManagement.Repository;
using HrManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace HrManagement.Repository
{
    public class DesignationRepository : GenericRepository<Designation>, IDesignationRepository
    {
        private readonly AppDbContext _context;

        public DesignationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Designation>> GetByCompanyId(Guid comId)
        {
            return await _context.Designations
                .Where(d => d.ComId == comId)
                .ToListAsync();
        }

        // Fix for CS0535: Implementing the missing interface method
        public async Task<IEnumerable<Designation>> GetByCompanyIdAsync(Guid comId)
        {
            return await GetByCompanyId(comId);
        }
    }
}
