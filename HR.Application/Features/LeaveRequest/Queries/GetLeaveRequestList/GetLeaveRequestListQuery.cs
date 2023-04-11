using MediatR;

namespace HR.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public class GetLeaveRequestListQuery : IRequest<List<LeaveRequestDto>>
{
    public bool IdLoggedInUser { get; set; }
}
