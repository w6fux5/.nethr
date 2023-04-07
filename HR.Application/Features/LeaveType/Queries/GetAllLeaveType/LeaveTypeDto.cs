namespace HR.Application.Features.LeaveType.Queries.GetAllLeaveType;

public class LeaveTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public int DefaultDays { get; set; }
}
