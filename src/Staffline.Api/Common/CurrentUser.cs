using System.Security.Claims;
using Staffline.Application.Common.Exceptions;
using Staffline.Domain.Enums;

namespace Staffline.Api.Common;

public sealed class CurrentUser(IHttpContextAccessor accessor)
{
    private ClaimsPrincipal? Principal => accessor.HttpContext?.User;

    private bool IsAuthenticated => Principal?.Identity?.IsAuthenticated == true;

    public Guid EmployeeId => Guid.TryParse(Principal?.FindFirstValue("sub"), out var id)
        ? id
        : throw new UnauthorizedException("Not Authenticated");

    public IReadOnlySet<RoleName>? Roles
    {
        get
        {
            var set = new HashSet<RoleName>();
            if (Principal is null) return null;

            foreach (var claim in Principal.FindAll("role"))
            {
                if (Enum.TryParse<RoleName>(claim.Value, out var role))
                    set.Add(role);
            }

            return set;
        }
    }

    public bool IsInRole(RoleName role) => Roles != null && Roles.Contains(role);
}