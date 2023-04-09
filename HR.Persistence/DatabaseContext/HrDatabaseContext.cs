using HR.Domain;
using HR.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HR.Persistence.DatabaseContext;

public class HrDatabaseContext : DbContext
{
	public HrDatabaseContext(DbContextOptions<HrDatabaseContext> options) : base(options)
	{
	}

	public DbSet<LeaveType> Tbl_LeaveTypes { get; set; }

	public DbSet<LeaveAllocation> Tbl_LeaveAllocation { get; set; }

	public DbSet<LeaveRequest> Tbl_LeaveRequest { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(HrDatabaseContext).Assembly);

		base.OnModelCreating(modelBuilder);
	}


	public override Task<int> SaveChangesAsync(CancellationToken cancellation = default)
	{
		foreach (var entry in base.ChangeTracker.Entries<BaseEntity>().Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
		{
			entry.Entity.UpdatedAt = DateTime.Now;

			if (entry.State == EntityState.Added)
			{
				entry.Entity.CreatedAt = DateTime.Now;
			}
		}

		return base.SaveChangesAsync(cancellation);
	}

}
