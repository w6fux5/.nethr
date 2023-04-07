using HR.Domain;

namespace HR.Application.Contracts.Persistence;

public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{

    Task AddAllocations(List<LeaveAllocation> allocations);

    Task<List<LeaveAllocation>> GetLeaveAllocations();

    Task<LeaveAllocation> GetLeaveAllocationById(int id);


    Task<LeaveAllocation> GetUserAllocations(string userId, int LeaveTypeId);

    Task<List<LeaveAllocation>> GetLeaveAllocationsByUserId(string userId);


    Task<bool> AllocationExists(string userId, int leaveTypeId, int period);








}


