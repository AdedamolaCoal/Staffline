using Microsoft.AspNetCore.Identity;
using Staffline.Application.Features.Auth.Interface;
using Staffline.Domain.Entities.Employees;

namespace Staffline.Infrastructure.Auth;

public sealed class PasswordService : IPasswordService
{
    private readonly PasswordHasher<Employee> _hasher = new();

    public string Hash(Employee employee, string password)
    {
        return _hasher.HashPassword(employee, password);
    }

    public bool Verify(Employee employee, string password)
    {
        var result = _hasher
            .VerifyHashedPassword(
                employee,
                employee.PasswordHash,
                password
            ) != PasswordVerificationResult.Failed;
        return result;
    }
}