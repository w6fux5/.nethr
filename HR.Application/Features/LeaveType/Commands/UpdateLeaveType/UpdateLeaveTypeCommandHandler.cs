using AutoMapper;
using HR.Application.Contracts.Logger;
using HR.Application.Contracts.Persistence;
using HR.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using MediatR;

namespace HR.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    private readonly IAppLogger<UpdateLeaveTypeCommandHandler> _logger;

    public UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepo, IAppLogger<UpdateLeaveTypeCommandHandler> logger)
    {
        _mapper = mapper;
        _leaveTypeRepo = leaveTypeRepo;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {

        // Valid incoming data
        var validator = new UpdateLeaveTypeCommandValidator(_leaveTypeRepo);
        var validatorResult = await validator.ValidateAsync(request);

        if (validatorResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(LeaveType), request.Id);
            throw new BadRequestException("Invalid Leave type", validatorResult);
        }

        // convert to domain object
        var leaveTypeToUpdate = _mapper.Map<Domain.LeaveType>(request);

        // add to datebase
        await _leaveTypeRepo.UpdateAsync(leaveTypeToUpdate);

        // return unit value
        return Unit.Value;
    }
}
