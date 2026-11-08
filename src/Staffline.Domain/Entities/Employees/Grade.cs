using Staffline.Domain.Common;

namespace Staffline.Domain.Entities.Employees;

public class Grade : BaseEntity
{
    public required string Code { get; set; }
    public int Level { get; set; }
}