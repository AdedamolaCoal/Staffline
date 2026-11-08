using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Staffline.Application.Common.Abstractions;
using Staffline.Application.Features.Auth.Interface;
using Staffline.Application.Features.Employees;
using Staffline.Infrastructure.Auth;
using Staffline.Infrastructure.Persistence;
using Staffline.Infrastructure.Persistence.Repositories;

namespace Staffline.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString,
        JwtOptions jwt
    )
    {
        services.AddDbContext<AppDbContext>(o => o.UseSqlServer(connectionString));
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());

        services.AddSingleton(jwt);
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IPasswordService, PasswordService>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IActiveEmployeeChecker, ActiveEmployeeChecker>();
        

        return services;
    }
}
