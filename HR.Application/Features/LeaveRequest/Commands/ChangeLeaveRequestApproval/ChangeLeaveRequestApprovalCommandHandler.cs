using AutoMapper;
using HR.Application.Contracts.Email;
using HR.Application.Contracts.Persistence;
using HR.Application.Exceptions;
using HR.Application.Models.Email;
using MediatR;

namespace HR.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    private readonly ILeaveRequestRepository _leaveRequestRepo;

    public ChangeLeaveRequestApprovalCommandHandler(IMapper mapper, IEmailSender emailSender, ILeaveTypeRepository leaveTypeRepo, ILeaveRequestRepository leaveRequestRepo)
    {
        _mapper = mapper;
        _emailSender = emailSender;
        _leaveTypeRepo = leaveTypeRepo;
        _leaveRequestRepo = leaveRequestRepo;
    }

    public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepo.GetByIdAsync(request.Id);

        if (leaveRequest is null)
        {
            throw new NotFoundException(nameof(leaveRequest), request.Id);
        }

        leaveRequest.Approved = request.Approved;

        await _leaveRequestRepo.UpdateAsync(leaveRequest);

        // if request is approved, get and update the employee's allcation


        // send confirmation email
        var email = new EmailMessage()
        {
            To = string.Empty,
            Body = $"The approval status for your leave request  for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} " + $"has been update successfully.",
            Subject = "Leave Request Approval Status Updated"
        };

        await _emailSender.SendEmail(email);


        return Unit.Value;
    }
}
