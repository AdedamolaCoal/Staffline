using Microsoft.EntityFrameworkCore;
using Staffline.Application.Features.Employees;
using Staffline.Domain.Entities.Employees;

namespace Staffline.Infrastructure.Persistence.Repositories;

public sealed class EmployeeRepository(
    AppDbContext db
    ) : Repository<Employee>(db), IEmployeeRepository
{
    public async Task<Employee?> GetByEmailAsync(string normalizedEmail, CancellationToken ct = default)
    {
        var data = await WithProfile().FirstOrDefaultAsync(e => e.Email == normalizedEmail, ct);
        return data;
    }

    public async Task<Employee?> GetWithProfileAsync(Guid id, CancellationToken ct = default)
    {
        var data = await WithProfile().FirstOrDefaultAsync(e => e.Id == id);
        return data;
    }

    private IQueryable<Employee> WithProfile() =>
        Db.Employees
            .Include(r => r.Roles)
            .Include(e => e.Department)
            .Include(e => e.Grade);
}