using HR.Application.Contracts.Persistence;
using HR.Domain;
using HR.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.Persistence.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(HrDatabaseContext context) : base(context)
        {
        }

        public async Task<bool> IsLeaveTypeUnique(string name)
        {
            return await _context.Tbl_LeaveTypes.AnyAsync(t => t.Name == name) == false;
        }
    }
}
