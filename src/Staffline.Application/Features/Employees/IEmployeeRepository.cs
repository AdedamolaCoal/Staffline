using Staffline.Application.Common.Abstractions;
using Staffline.Domain.Entities.Employees;

namespace Staffline.Application.Features.Employees;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<Employee?> GetByEmailAsync(string normalizedEmail, CancellationToken ct = default);
    Task<Employee?> GetWithProfileAsync(Guid id, CancellationToken ct = default);
}