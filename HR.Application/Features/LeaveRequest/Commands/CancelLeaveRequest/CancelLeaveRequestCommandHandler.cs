using HR.Application.Contracts.Email;
using HR.Application.Contracts.Persistence;
using HR.Application.Exceptions;
using HR.Application.Models.Email;
using MediatR;

namespace HR.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;

public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Unit>
{
    private readonly IEmailSender _emailSender;
    private readonly ILeaveRequestRepository _leaveRequestRepo;

    public CancelLeaveRequestCommandHandler(IEmailSender emailSender, ILeaveRequestRepository leaveRequestRepo)
    {
        _emailSender = emailSender;
        _leaveRequestRepo = leaveRequestRepo;
    }

    public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepo.GetByIdAsync(request.Id);

        if (leaveRequest is null)
        {
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        leaveRequest.Cancelled = true;

        // Re-evaluate the employee's allocations for the leave type

        // Send confirmatin email

        var email = new EmailMessage()
        {
            To = string.Empty,
            Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} " + $"has been cancelled successfully.",
            Subject = "Leave Request Cancelled"
        };
        await _emailSender.SendEmail(email);

        return Unit.Value;
    }
}
