using HR.Application.Features.LeaveRequest.Shared;
using HR.Application.Features.LeaveType.Queries.GetAllLeaveType;
using HR.Application.Models.Identity;

namespace HR.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public class LeaveRequestDto : BaseLeaveRequest
{
    public int Id { get; set; }

    public Employee Employee { get; set; }

    public string RequestingEmployeeId { get; set; }

    public LeaveTypeDto LeaveType { get; set; }

    public DateTime DateRequested { get; set; }

    public bool? Approved { get; set; }
}
