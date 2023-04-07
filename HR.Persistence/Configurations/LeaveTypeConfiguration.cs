using HR.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.Persistence.Configurations
{
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.HasData(
                new LeaveType
                {
                    Id = 1,
                    Name = "Vacation",
                    DefaultDays = 10,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                }
            );

            // 在數據庫驗證
            builder.Property(q => q.Name).IsRequired().HasMaxLength(100);
        }
    }
}
