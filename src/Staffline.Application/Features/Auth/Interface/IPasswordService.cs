using Staffline.Domain.Entities.Employees;

namespace Staffline.Application.Features.Auth.Interface;

public interface IPasswordService
{
    string Hash(Employee employee, string password);
    bool Verify(Employee employee, string password);
}