using Staffline.Application.Common.Abstractions;
using Staffline.Application.Common.Exceptions;
using Staffline.Application.Features.Auth.Contracts;
using Staffline.Application.Features.Auth.Interface;
using Staffline.Application.Features.Employees;
using Staffline.Domain.Entities.Employees;

namespace Staffline.Application.Features.Auth.Services;

public sealed class AuthService(
    IEmployeeRepository repository,
    IJwtTokenGenerator jwt,
    IPasswordService password,
    ICurrentUser currentUser) : IAuthService
{
    public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken ct = default)
    {
        var employee = await repository.GetByEmailAsync(request.Email.Trim().ToLowerInvariant(), ct);
        if (employee is null || !employee.IsActive || !password.Verify(employee, request.Password))
            throw new UnauthorizedException("Invalid email or password.");

        var token = jwt.Generate(employee);
        return new LoginResponse(
            token.Token,
            token.ExpiresAtUtc,
            employee.ToProfileDto()
        );
    }

    public async Task<EmployeeProfile> GetEmployeeProfileAsync(CancellationToken ct = default)
    {
        var id = currentUser.EmployeeId;
        var employee = await repository.GetWithProfileAsync(id, ct)
                       ?? throw new NotFoundException(nameof(Employee), id);
        return employee.ToProfileDto();
    }
}