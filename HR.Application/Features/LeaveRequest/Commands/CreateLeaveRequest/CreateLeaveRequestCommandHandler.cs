using AutoMapper;
using HR.Application.Contracts.Email;
using HR.Application.Contracts.Logger;
using HR.Application.Contracts.Persistence;
using HR.Application.Exceptions;
using HR.Application.Models.Email;
using MediatR;

namespace HR.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
{
    private readonly IEmailSender _emailSender;
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    private readonly ILeaveRequestRepository _leaveRequestRepo;
    private readonly IAppLogger<CreateLeaveRequestCommandHandler> _logger;

    public CreateLeaveRequestCommandHandler(IEmailSender emailSender, IMapper mapper, ILeaveTypeRepository leaveTypeRepo, ILeaveRequestRepository leaveRequestRepo, IAppLogger<CreateLeaveRequestCommandHandler> logger)
    {
        _emailSender = emailSender;
        _mapper = mapper;
        _leaveTypeRepo = leaveTypeRepo;
        _leaveRequestRepo = leaveRequestRepo;
        _logger = logger;
    }

    public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepo);

        var validatorResult = await validator.ValidateAsync(request, cancellationToken);

        if (validatorResult.Errors.Any())
        {
            throw new BadRequestException("Invalid leave request", validatorResult);
        }

        // Get requesting employee's id

        // Check on employee's allocation

        // if allocations are not enough, return validation error with message

        // Create leave request
        var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request);
        await _leaveRequestRepo.CreateAsync(leaveRequest);

        // Send Confirmation email
        try
        {
            var email = new EmailMessage()
            {
                To = string.Empty,
                Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} " + $"has been update successfully.",
                Subject = "Leave Request Submitted"
            };

            await _emailSender.SendEmail(email);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
        }


        return Unit.Value;
    }
}
