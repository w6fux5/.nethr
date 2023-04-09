﻿using HR.Application.Features.LeaveRequest.Shared;
using HR.Application.Features.LeaveType.Queries.GetAllLeaveType;

namespace HR.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;

public class LeaveRequestDetailsDto : BaseLeaveRequest
{
    public string RequestingEmployeeId { get; set; }

    public LeaveTypeDto LeaveType { get; set; }

    public DateTime DateRequested { get; set; }

    public string RequestComments { get; set; }

    public DateTime? DateActioned { get; set; }

    public bool? Approved { get; set; }

    public bool Cancelled { get; set; }
}
