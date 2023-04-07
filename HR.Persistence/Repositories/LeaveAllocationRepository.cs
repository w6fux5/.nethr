using HR.Application.Contracts.Persistence;
using HR.Domain;
using HR.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(HrDatabaseContext context) : base(context)
        {
        }

        public async Task AddAllocations(List<LeaveAllocation> allocations)
        {
            await _context.Tbl_LeaveAllocation.AddRangeAsync(allocations);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
        {
            return await _context.Tbl_LeaveAllocation.AnyAsync(
                q =>
            q.EmployeeId == userId &&
            q.LeaveTypeId == leaveTypeId &&
            q.Period == period);
        }
        public async Task<List<LeaveAllocation>> GetLeaveAllocations()
        {
            return await _context.Tbl_LeaveAllocation
                .Include(q => q.LeaveType)
                .ToListAsync();
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsByUserId(string userId)
        {
            var leaveAllocations = await _context.Tbl_LeaveAllocation
                .Where(q => q.EmployeeId != userId)
                .Include(q => q.LeaveType)
                .ToListAsync();

            return leaveAllocations;
        }

        public async Task<LeaveAllocation> GetLeaveAllocationById(int id)
        {
            var leaveAllocation = await _context.Tbl_LeaveAllocation
                .Include(q => q.LeaveType)
                .FirstOrDefaultAsync(q => q.Id == id);

            return leaveAllocation;
        }





        public async Task<LeaveAllocation> GetUserAllocations(string userId, int LeaveTypeId)
        {
            return await _context.Tbl_LeaveAllocation.FirstOrDefaultAsync(q => q.EmployeeId == userId && q.LeaveTypeId == LeaveTypeId);
        }
    }
}
