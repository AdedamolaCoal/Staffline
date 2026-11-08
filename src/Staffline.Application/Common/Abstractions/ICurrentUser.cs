using Staffline.Domain.Enums;

namespace Staffline.Application.Common.Abstractions;

public interface ICurrentUser
{
    bool IsAuthenticated { get; }
    Guid EmployeeId { get; }
    IReadOnlySet<RoleName> Roles { get; }
    bool IsInRole(RoleName role);
}