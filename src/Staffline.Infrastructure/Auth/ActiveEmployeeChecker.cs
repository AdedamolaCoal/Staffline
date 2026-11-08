using Microsoft.EntityFrameworkCore;
using Staffline.Application.Features.Auth.Interface;
using Staffline.Infrastructure.Persistence;

namespace Staffline.Infrastructure.Auth;

public sealed class ActiveEmployeeChecker(AppDbContext db) : IActiveEmployeeChecker
{
    public Task<bool> IsActiveAsync(Guid employeeId, CancellationToken ct = default)
    {
        var result = db.Employees
            .AsNoTracking()
            .AnyAsync(e => e.Id == employeeId, ct);
        return result;
    }
}