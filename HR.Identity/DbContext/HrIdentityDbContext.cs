﻿using HR.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HR.Identity.DbContext;

public class HrIdentityDbContext : IdentityDbContext<ApplicationUser>
{

    public HrIdentityDbContext(DbContextOptions<HrIdentityDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(HrIdentityDbContext).Assembly);
    }
}
