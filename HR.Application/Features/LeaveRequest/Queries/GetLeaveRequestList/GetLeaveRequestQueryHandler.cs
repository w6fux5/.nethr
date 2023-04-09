using AutoMapper;
using HR.Application.Contracts.Persistence;
using MediatR;

namespace HR.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public class GetLeaveRequestQueryHandler : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _leaveRequestRepo;

    public GetLeaveRequestQueryHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepo)
    {
        _mapper = mapper;
        _leaveRequestRepo = leaveRequestRepo;
    }
    public async Task<List<LeaveRequestDto>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
    {
        // check if it is logged in employee

        var leaveRequestList = await _leaveRequestRepo.GetLeaveRequestsList();

        var requestListDto = _mapper.Map<List<LeaveRequestDto>>(leaveRequestList);

        // Fill requests with employee information

        return requestListDto;
    }
}
