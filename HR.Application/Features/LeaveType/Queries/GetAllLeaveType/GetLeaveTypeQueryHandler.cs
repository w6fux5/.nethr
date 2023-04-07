using AutoMapper;
using HR.Application.Contracts.Logger;
using HR.Application.Contracts.Persistence;
using MediatR;

namespace HR.Application.Features.LeaveType.Queries.GetAllLeaveType;

public class GetLeaveTypeQueryHandler : IRequestHandler<GetLeaveTypeQuery, List<LeaveTypeDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    private readonly IAppLogger<GetLeaveTypeQueryHandler> _logger;

    public GetLeaveTypeQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepo, IAppLogger<GetLeaveTypeQueryHandler> logger)
    {
        _mapper = mapper;
        _leaveTypeRepo = leaveTypeRepo;
        _logger = logger;
    }
    public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypeQuery request, CancellationToken cancellationToken)
    {
        // query database
        var leaveTypes = await _leaveTypeRepo.GetAsync();

        // convert data objects to DTO objects
        var data = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

        // return list of dto object
        _logger.LogInformation("Leave types were retrieved successfully");
        return data;

    }
}
