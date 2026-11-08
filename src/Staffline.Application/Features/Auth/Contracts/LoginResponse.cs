namespace Staffline.Application.Features.Auth.Contracts;

public sealed record LoginResponse(
    string AccessToken,
    DateTime ExpiresAtUtc,
    EmployeeProfile Employee
);