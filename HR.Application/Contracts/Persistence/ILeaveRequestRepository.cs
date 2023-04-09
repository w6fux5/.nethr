using HR.Domain;

namespace HR.Application.Contracts.Persistence;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<LeaveRequest> GetLeaveRequestDetails(int id);

    Task<List<LeaveRequest>> GetLeaveRequestsList();

    Task<List<LeaveRequest>> GetLeaveRequestsByUserId(string userId);
}


