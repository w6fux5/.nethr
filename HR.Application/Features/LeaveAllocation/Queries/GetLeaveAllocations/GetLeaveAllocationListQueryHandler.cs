using AutoMapper;
using HR.Application.Contracts.Persistence;
using MediatR;

namespace HR.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

public class GetLeaveAllocationListQueryHandler : IRequestHandler<GetLeaveAllocationListQuery, List<LeaveAllocationDto>>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepo;
    private readonly IMapper _mapper;

    public GetLeaveAllocationListQueryHandler(ILeaveAllocationRepository leaveAllocationRepo, IMapper mapper)
    {
        _leaveAllocationRepo = leaveAllocationRepo;
        _mapper = mapper;
    }
    public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationListQuery request, CancellationToken cancellationToken)
    {
        // To Add later
        // - Get records for specific user
        // - Get allocations per emplyee

        var leaveAllocations = await _leaveAllocationRepo.GetLeaveAllocations();

        var allocations = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);

        return allocations;
    }
}
