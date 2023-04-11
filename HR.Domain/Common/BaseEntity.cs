

namespace HR.Domain.Common;

public class BaseEntity
{
    public int Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? ModifiedBy { get; set; }
}
