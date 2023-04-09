using HR.Application.Features.LeaveRequest.Shared;
using HR.Application.Features.LeaveType.Queries.GetAllLeaveType;

namespace HR.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public class LeaveRequestDto : BaseLeaveRequest
{
    public string RequestingEmployeeId { get; set; }

    public LeaveTypeDto LeaveType { get; set; }

    public DateTime DateRequested { get; set; }

    public bool? Approved { get; set; }
}
