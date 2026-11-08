using Microsoft.EntityFrameworkCore;
using Staffline.Application.Common.Abstractions;
using Staffline.Domain.Common;
using Staffline.Domain.Entities.Employees;

namespace Staffline.Infrastructure.Persistence;

public sealed class AppDbContext(
    DbContextOptions<AppDbContext> options
    ) : DbContext(options), IUnitOfWork
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Grade> Grades => Set<Grade>();
    public DbSet<EmployeeRole> EmployeeRoles => Set<EmployeeRole>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                     .Where((t) => typeof(BaseEntity).IsAssignableFrom(t.ClrType)))
        {
            modelBuilder.Entity(entityType.ClrType).Property(nameof(BaseEntity.RowVersion)).IsRowVersion();
        }
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
    }

    public override Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        var now = DateTime.UtcNow;
        foreach (var entry in ChangeTracker.Entries<BaseEntity>()
                     .Where(e => e.State == EntityState.Modified))
        {
            entry.Entity.UpdatedAt = now;
        }

        return base.SaveChangesAsync(ct);
    }
}