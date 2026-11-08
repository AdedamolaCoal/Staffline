using Staffline.Application.Features.Auth.Contracts;
using Staffline.Domain.Entities.Employees;

namespace Staffline.Application.Features.Auth.Interface;

public interface IJwtTokenGenerator
{
    AccessToken Generate(Employee employee);
}