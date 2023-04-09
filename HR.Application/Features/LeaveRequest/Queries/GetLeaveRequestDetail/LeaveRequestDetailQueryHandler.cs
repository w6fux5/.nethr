using AutoMapper;
using HR.Application.Contracts.Persistence;
using MediatR;

namespace HR.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;

public class LeaveRequestDetailQueryHandler : IRequestHandler<LeaveRequestDetailQuery, LeaveRequestDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _leaveRequestRepo;

    public LeaveRequestDetailQueryHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepo)
    {
        _mapper = mapper;
        _leaveRequestRepo = leaveRequestRepo;
    }

    public async Task<LeaveRequestDetailsDto> Handle(LeaveRequestDetailQuery request, CancellationToken cancellationToken)
    {

        var leaveRequest = await _leaveRequestRepo.GetLeaveRequestDetails(request.Id);

        var leaveRequestDto = _mapper.Map<LeaveRequestDetailsDto>(leaveRequest);

        // Add Employee details as needed

        return leaveRequestDto;

    }
}
