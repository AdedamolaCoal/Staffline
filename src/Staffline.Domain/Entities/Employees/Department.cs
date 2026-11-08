using Staffline.Domain.Common;

namespace Staffline.Domain.Entities.Employees;

public class Department : BaseEntity
{
    public string Name { get; set; } = string.Empty;
}