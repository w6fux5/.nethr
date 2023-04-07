using HR.Application.Contracts.Persistence;
using HR.Application.Exceptions;
using MediatR;

namespace HR.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    private readonly ILeaveAllocationRepository _leaveTypeRepo;

    public DeleteLeaveTypeCommandHandler(ILeaveAllocationRepository leaveTypeRepo)
    {
        _leaveTypeRepo = leaveTypeRepo;
    }
    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        // retrieve to domain object
        var leaveTypeToDelete = await _leaveTypeRepo.GetByIdAsync(request.Id);

        // verify that record exists
        if (leaveTypeToDelete == null)
        {
            throw new NotFoundException(nameof(LeaveType), request.Id);
        }

        // add to database
        await _leaveTypeRepo.DeleteAsync(leaveTypeToDelete);

        // return unit value
        return Unit.Value;
    }
}
