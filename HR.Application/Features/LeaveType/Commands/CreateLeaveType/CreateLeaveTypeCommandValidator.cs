using FluentValidation;
using HR.Application.Contracts.Persistence;

namespace HR.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepo;

    public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepo)
    {
        _leaveTypeRepo = leaveTypeRepo;

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} id required")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");

        RuleFor(p => p.DefaultDays)
            .LessThan(100).WithMessage("{PropertyName} cannot exceed 100")
            .GreaterThan(1).WithMessage("{PropertyName} cannot be less than 1");


        RuleFor(q => q).MustAsync(LeaqveTypeNameUnique).WithMessage("Leave type already exists");
    }

    private Task<bool> LeaqveTypeNameUnique(CreateLeaveTypeCommand command, CancellationToken token)
    {
        return _leaveTypeRepo.IsLeaveTypeUnique(command.Name);
    }
}
