using MediatR;

namespace HR.Application.Features.LeaveType.Queries.GetAllLeaveType;

//public class GetLeaveTypeQuery : IRequest<List<LeaveTypeDto>>
//{

//}

public record GetLeaveTypeQuery : IRequest<List<LeaveTypeDto>>;
