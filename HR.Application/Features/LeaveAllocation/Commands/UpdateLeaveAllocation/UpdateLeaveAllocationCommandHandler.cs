using AutoMapper;
using HR.Application.Contracts.Persistence;
using HR.Application.Exceptions;
using MediatR;

namespace HR.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    private readonly ILeaveAllocationRepository _leaveAllocationRepo;

    public UpdateLeaveAllocationCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepo, ILeaveAllocationRepository leaveAllocationRepo)
    {
        _mapper = mapper;
        _leaveTypeRepo = leaveTypeRepo;
        _leaveAllocationRepo = leaveAllocationRepo;
    }
    public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveAllocationCommandValidator(_leaveTypeRepo, _leaveAllocationRepo);

        var validaotrResult = await validator.ValidateAsync(request);

        if (validaotrResult.Errors.Any())
        {
            throw new BadRequestException("Invalid leave allocation", validaotrResult);
        }

        var leaveAllocation = await _leaveAllocationRepo.GetByIdAsync(request.Id);

        if (leaveAllocation is null)
        {
            throw new NotFoundException(nameof(LeaveAllocation), request.Id);
        }

        _mapper.Map(request, leaveAllocation);

        await _leaveAllocationRepo.UpdateAsync(leaveAllocation);

        return Unit.Value;
    }
}
