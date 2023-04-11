using AutoMapper;
using HR.Application.Contracts.Email;
using HR.Application.Contracts.Identity;
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
    private readonly ILeaveAllocationRepository _leaveAllocationRepo;
    private readonly IUserService _userService;
    private readonly IAppLogger<CreateLeaveRequestCommandHandler> _logger;

    public CreateLeaveRequestCommandHandler(
        IEmailSender emailSender,
        IMapper mapper,
        ILeaveTypeRepository leaveTypeRepo,
        ILeaveRequestRepository leaveRequestRepo,
        ILeaveAllocationRepository leaveAllocationRepo,
        IUserService userService,
        IAppLogger<CreateLeaveRequestCommandHandler> logger)
    {
        _emailSender = emailSender;
        _mapper = mapper;
        _leaveTypeRepo = leaveTypeRepo;
        _leaveRequestRepo = leaveRequestRepo;
        _leaveAllocationRepo = leaveAllocationRepo;
        _userService = userService;
        _logger = logger;
    }

    public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepo);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid leave request", validationResult);
        }

        // Get requesting employee's id
        var employeeId = _userService.UserId;

        // Check on employee's allocation
        var allocation = await _leaveAllocationRepo.GetUserAllocations(employeeId, request.LeaveTypeId);

        // if allocations are not enough, return validation error with message
        if (allocation is null)
        {
            validationResult.Errors.Add(
                new FluentValidation
                .Results
                .ValidationFailure(nameof(request.LeaveTypeId), "You do not have any allocations for this leave type."));

            throw new BadRequestException("Invalid Leave Resuest", validationResult);
        }

        int daysRequested = (int)(request.EndDate - request.StartDate).TotalDays;

        if (daysRequested > allocation.NumberOfDays)
        {
            validationResult.Errors.Add(
                    new FluentValidation
                    .Results
                    .ValidationFailure(nameof(request.EndDate), "You do not have enough days for this request"));

            throw new BadRequestException("Invalid Leave Resuest", validationResult);
        }


        // Create leave request
        var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request);
        leaveRequest.RequestingEmployeeId = employeeId;
        leaveRequest.DateRequested = DateTime.Now;
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
