using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Staffline.Application.Features.Auth.Interface;
using Staffline.Application.Features.Auth.Services;

namespace Staffline.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        // SERVICE REGISTRATIONS
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}
