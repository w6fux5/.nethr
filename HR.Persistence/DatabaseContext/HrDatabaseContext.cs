using HR.Application.Contracts.Identity;
using HR.Domain;
using HR.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HR.Persistence.DatabaseContext;

public class HrDatabaseContext : DbContext
{
	private readonly IUserService _userService;

	public HrDatabaseContext(DbContextOptions<HrDatabaseContext> options, IUserService userService) : base(options)
	{
		_userService = userService;
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
			entry.Entity.ModifiedBy = _userService.UserId;

			if (entry.State == EntityState.Added)
			{
				entry.Entity.CreatedAt = DateTime.Now;
				entry.Entity.CreateBy = _userService.UserId;
			}
		}

		return base.SaveChangesAsync(cancellation);
	}

}
