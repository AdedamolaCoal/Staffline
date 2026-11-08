using Staffline.Application.Features.Auth.Contracts;

namespace Staffline.Application.Features.Auth.Interface;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken ct = default);
    Task<EmployeeProfile> GetEmployeeProfileAsync(CancellationToken ct = default);
}