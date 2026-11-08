using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Staffline.Domain.Entities.Employees;

namespace Staffline.Infrastructure.Persistence.Configurations.Employee;

public sealed class GradeConfigurations : IEntityTypeConfiguration<Grade>
{
    public void Configure(EntityTypeBuilder<Grade> builder)
    {
        builder.ToTable("Grades");
        builder.Property(x => x.Code)
            .HasMaxLength(20)
            .IsRequired();
        builder.HasIndex(x => x.Code)
            .IsUnique();
    }
}