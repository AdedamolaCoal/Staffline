using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Staffline.Domain.Entities.Employees;

namespace Staffline.Infrastructure.Persistence.Configurations.Employee;

public class EmployeeRoleConfiguration : IEntityTypeConfiguration<EmployeeRole>
{
    public void Configure(EntityTypeBuilder<EmployeeRole> builder)
    {
        builder.ToTable("EmployeeRoles");
        builder.HasKey(x => new { x.EmployeeId, x.Role });
        builder.Property(x => x.EmployeeId)
            .HasConversion<string>()
            .HasMaxLength(20);
    }
}