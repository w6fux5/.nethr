using HR.Application.Contracts.Identity;
using HR.Application.Contracts.Persistence;
using HR.Application.Exceptions;
using MediatR;

namespace HR.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepo;
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    private readonly IUserService _userService;

    public CreateLeaveAllocationCommandHandler(
        ILeaveAllocationRepository leaveAllocationRepo,
        ILeaveTypeRepository leaveTypeRepo,
        IUserService userService)
    {
        _leaveAllocationRepo = leaveAllocationRepo;
        _leaveTypeRepo = leaveTypeRepo;
        _userService = userService;
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
        var employees = await _userService.GetEmployees();

        // Get Period
        var period = DateTime.Now.Year;

        // Assign Allocations if an allocation doesn't already exist for period and leave type
        var allocations = new List<Domain.LeaveAllocation>();

        foreach (var emp in employees)
        {
            var allocationExists = await _leaveAllocationRepo.AllocationExists(emp.Id, request.LeaveTypesId, period);

            if (allocationExists == false)
            {
                allocations.Add(new Domain.LeaveAllocation
                {
                    EmployeeId = emp.Id,
                    LeaveTypeId = leaveType.Id,
                    NumberOfDays = leaveType.DefaultDays,
                    Period = period
                });
            }

        }

        if (allocations.Any())
        {
            await _leaveAllocationRepo.AddAllocations(allocations);
        }


        return Unit.Value;
    }
}
