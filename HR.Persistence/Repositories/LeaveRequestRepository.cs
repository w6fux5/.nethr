using HR.Application.Contracts.Persistence;
using HR.Domain;
using HR.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.Persistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        public LeaveRequestRepository(HrDatabaseContext context) : base(context)
        {
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
        {
            var leaveRequests = await _context
                .Tbl_LeaveRequest
                .Include(q => q.LeaveType)
                .ToListAsync();

            return leaveRequests;
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
        {
            var leaveRequests = await _context
                .Tbl_LeaveRequest
                .Where(q => q.RequestingEmployeeId == userId)
                .Include(x => x.LeaveType)
                .ToListAsync();

            return leaveRequests;
        }

        public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
        {

            var leaveRequest = await _context
                .Tbl_LeaveRequest
                .Include(q => q.LeaveType)
                .FirstOrDefaultAsync(x => x.Id == id);

            return leaveRequest;
        }
    }
}
