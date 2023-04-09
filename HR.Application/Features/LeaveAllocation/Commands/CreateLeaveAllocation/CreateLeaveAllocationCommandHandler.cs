using AutoMapper;
using HR.Application.Contracts.Persistence;
using HR.Application.Exceptions;
using MediatR;

namespace HR.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepo;
    private readonly ILeaveTypeRepository _leaveTypeRepo;

    public CreateLeaveAllocationCommandHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepo, ILeaveTypeRepository leaveTypeRepo)
    {
        _mapper = mapper;
        _leaveAllocationRepo = leaveAllocationRepo;
        _leaveTypeRepo = leaveTypeRepo;
    }

    public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepo);
        var validatorResult = await validator.ValidateAsync(request);

        if (validatorResult.Errors.Any())
        {
            throw new BadRequestException("Invalid Leave Allocation request", validatorResult);
        }

        // Get leave type for allocations
        var leaveType = await _leaveTypeRepo.GetByIdAsync(request.LeaveTypesId);

        // Get Employees

        // Get Period

        // Assign Allocations
        var leaveAllocation = _mapper.Map<Domain.LeaveAllocation>(request);
        await _leaveAllocationRepo.CreateAsync(leaveAllocation);
        return Unit.Value;
    }
}
