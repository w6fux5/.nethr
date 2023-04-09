using AutoMapper;
using HR.Application.Contracts.Email;
using HR.Application.Contracts.Logger;
using HR.Application.Contracts.Persistence;
using HR.Application.Exceptions;
using HR.Application.Models.Email;
using MediatR;

namespace HR.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    private readonly ILeaveRequestRepository _leaveRequestRepo;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<UpdateLeaveRequestCommandHandler> _logger;

    public UpdateLeaveRequestCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepo, ILeaveRequestRepository leaveRequestRepo, IEmailSender emailSender, IAppLogger<UpdateLeaveRequestCommandHandler> logger)
    {
        _mapper = mapper;
        _leaveTypeRepo = leaveTypeRepo;
        _leaveRequestRepo = leaveRequestRepo;
        _emailSender = emailSender;
        _logger = logger;
    }
    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepo.GetByIdAsync(request.Id);

        if (leaveRequest is null)
        {
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        var validator = new UpdateLeaveRequestCommandValidator(_leaveTypeRepo, _leaveRequestRepo);
        var validatorResult = await validator.ValidateAsync(request, cancellationToken);

        if (validatorResult.Errors.Any())
        {
            throw new BadRequestException("Invalid leave request", validatorResult);
        }

        _mapper.Map(request, leaveRequest);

        await _leaveRequestRepo.UpdateAsync(leaveRequest);

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
