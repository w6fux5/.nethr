using AutoMapper;
using HR.Application.Contracts.Persistence;
using MediatR;

namespace HR.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class LeaveAllocationDetailQueryHandler : IRequestHandler<LeaveAllocationDetailQuery, LeaveAllocationDetailsDto>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepo;
    private readonly IMapper _mapper;

    public LeaveAllocationDetailQueryHandler(ILeaveAllocationRepository leaveAllocationRepo, IMapper mapper)
    {
        _leaveAllocationRepo = leaveAllocationRepo;
        _mapper = mapper;
    }

    public async Task<LeaveAllocationDetailsDto> Handle(LeaveAllocationDetailQuery request, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _leaveAllocationRepo.GetLeaveAllocationDetailsById(request.Id);
        return _mapper.Map<LeaveAllocationDetailsDto>(leaveAllocation);
    }
}
