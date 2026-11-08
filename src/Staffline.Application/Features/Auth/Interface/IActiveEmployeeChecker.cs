namespace Staffline.Application.Features.Auth.Interface;

public interface IActiveEmployeeChecker
{
    Task<bool> IsActiveAsync(Guid employeeId, CancellationToken ct = default);
}