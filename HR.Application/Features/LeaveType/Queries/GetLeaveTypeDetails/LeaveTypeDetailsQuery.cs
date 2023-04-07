using MediatR;

namespace HR.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;


public record LeaveTypesDetailsQuery(int Id) : IRequest<LeaveTypeDetailsDto>;
