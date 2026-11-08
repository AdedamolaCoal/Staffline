using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Staffline.Domain.Entities.Employees;

namespace Staffline.Infrastructure.Persistence.Configurations.Employee;

public sealed class DepartmentConfigurations : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("Departments");
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.HasIndex(x => x.Name)
            .IsUnique();
    }
}