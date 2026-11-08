using Staffline.Domain.Common;
using Staffline.Domain.Enums;

namespace Staffline.Domain.Entities.Employees;

public class Employee : BaseEntity
{
    public required string EmployeeNumber { get; set; }
    public required string Fullname { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public bool IsActive { get; set; }

    public Guid DepartmentId { get; set; }
    public Department Department { get; set; } = null!;

    public Guid GradeId { get; set; }
    public Grade Grade { get; set; } = null!;

    public Guid? ManagerId { get; set; }
    public Employee? Manager { get; set; }
    public ICollection<Employee> DirectReports { get; set; } = [];

    public ICollection<EmployeeRole> Roles { get; set; } = [];

    public bool HasRole(RoleName role) => Roles.Any(r => r.Role == role);
    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
}