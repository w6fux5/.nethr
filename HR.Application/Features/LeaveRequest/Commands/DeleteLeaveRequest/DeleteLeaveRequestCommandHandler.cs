using HR.Application.Contracts.Persistence;
using HR.Application.Exceptions;
using MediatR;

namespace HR.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;

public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepo;

    public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepo)
    {
        _leaveRequestRepo = leaveRequestRepo;
    }

    public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepo.GetByIdAsync(request.Id);

        if (leaveRequest == null)
        {
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        await _leaveRequestRepo.DeleteAsync(leaveRequest);
        return Unit.Value;
    }
}
