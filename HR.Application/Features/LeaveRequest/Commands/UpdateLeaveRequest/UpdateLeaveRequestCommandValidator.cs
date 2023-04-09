using FluentValidation;
using HR.Application.Contracts.Persistence;
using HR.Application.Features.LeaveRequest.Shared;

namespace HR.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandValidator : AbstractValidator<UpdateLeaveRequestCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    private readonly ILeaveRequestRepository _leaveRequestRpo;

    public UpdateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepo, ILeaveRequestRepository leaveRequestRpo)
    {
        _leaveTypeRepo = leaveTypeRepo;
        _leaveRequestRpo = leaveRequestRpo;

        Include(new BaseLeaveRequestValidator(_leaveTypeRepo));

        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(LeaveRequestMustExist)
            .WithMessage("{PropertyName} must be present");

    }

    private async Task<bool> LeaveRequestMustExist(int id, CancellationToken arg2)
    {
        var leaveRequest = await _leaveRequestRpo.GetByIdAsync(id);

        return leaveRequest != null;
    }
}
