using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EmployeeEntity = Staffline.Domain.Entities.Employees.Employee;

namespace Staffline.Infrastructure.Persistence.Configurations.Employee;

public sealed class EmployeeConfigurations : IEntityTypeConfiguration<EmployeeEntity>
{
    public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
    {
        builder.ToTable("Employees");
        builder.Property(x => x.EmployeeNumber)
            .IsRequired()
            .HasMaxLength(32);
        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(256);
        builder.Property(x => x.Fullname)
            .IsRequired()
            .HasMaxLength(256);
        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(512);

        // RELATIONSHIP
        builder.HasOne(x => x.Department)
            .WithMany()
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Grade)
            .WithMany()
            .HasForeignKey(x => x.GradeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Manager)
            .WithMany(x => x.DirectReports)
            .HasForeignKey(x => x.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Roles)
            .WithOne(x => x.Employee)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        // INDEX
        builder.HasIndex(x => x.EmployeeNumber)
            .IsUnique();
        builder.HasIndex(x => x.Email)
            .IsUnique();
    }
}