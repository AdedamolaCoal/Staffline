using Staffline.Domain.Entities.Employees;

namespace Staffline.Application.Features.Auth.Contracts;

public static class EmployeeMapping
{
    public static EmployeeProfile ToProfileDto(this Employee e) => new(
        e.Id,
        e.EmployeeNumber,
        e.Fullname,
        e.Department.Name,
        e.Grade.Code,
        e.ManagerId,
        [
            .. e.Roles
                .Select(r => r.Role.ToString())
                .OrderBy(r => r)
        ]);
}