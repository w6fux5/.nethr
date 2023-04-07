using AutoMapper;
using HR.Application.Contracts.Persistence;
using HR.Application.Exceptions;
using MediatR;

namespace HR.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public class LeaveTypeDetailsQueryHandler : IRequestHandler<LeaveTypesDetailsQuery, LeaveTypeDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepo;

    public LeaveTypeDetailsQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepo)
    {
        _mapper = mapper;
        _leaveTypeRepo = leaveTypeRepo;
    }
    public async Task<LeaveTypeDetailsDto> Handle(LeaveTypesDetailsQuery request, CancellationToken cancellationToken)
    {
        // query ths database
        var leaveType = await _leaveTypeRepo.GetByIdAsync(request.Id);

        // verify that record exists
        if (leaveType == null)
        {
            throw new NotFoundException(nameof(LeaveType), request.Id);
        }

        // Convert data object to dtp object
        var data = _mapper.Map<LeaveTypeDetailsDto>(leaveType);


        // return dto object
        return data;
    }
}
