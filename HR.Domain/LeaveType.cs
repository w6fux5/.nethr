using HR.Domain.Common;

namespace HR.Domain;

public class LeaveType : BaseEntity
{
    public string Name { get; set; } = String.Empty;
    public int DefaultDays { get; set; }
}
