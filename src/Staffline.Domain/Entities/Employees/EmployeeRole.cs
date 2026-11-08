using Staffline.Domain.Common;
using Staffline.Domain.Enums;

namespace Staffline.Domain.Entities.Employees;

public class EmployeeRole : BaseEntity
{
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public RoleName Role { get; set; }
}