namespace Staffline.Application.Features.Auth.Contracts;

public sealed record EmployeeProfile(
    Guid Id,
    string EmployeeNumber,
    string FullName,
    string Department,
    string Grade,
    Guid? ManagerId,
    IReadOnlyList<string> Roles
);