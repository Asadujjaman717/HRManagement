using HrManagement.Data;
using HrManagement.Models;
using HrManagement.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HrManagement.Repository
{
    public class ShiftRepository : GenericRepository<Shift>, IShiftRepository
    {
        private readonly AppDbContext _context;

        public ShiftRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Shift>> GetByCompanyIdAsync(Guid comId)
        {
            return await _context.Shifts
                .Include(s => s.Company)
                .Where(s => s.ComId == comId)
                .ToListAsync();
        }
    }
}
