using FluentValidation;
using HR.Application.Contracts.Persistence;
using HR.Application.Features.LeaveRequest.Shared;

namespace HR.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandValidator : AbstractValidator<CreateLeaveRequestCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepo;

    public CreateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepo)
    {
        _leaveTypeRepo = leaveTypeRepo;

        Include(new BaseLeaveRequestValidator(_leaveTypeRepo));
    }
}
