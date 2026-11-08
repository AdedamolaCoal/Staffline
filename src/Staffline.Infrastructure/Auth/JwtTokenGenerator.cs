using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Staffline.Application.Features.Auth.Contracts;
using Staffline.Application.Features.Auth.Interface;
using Staffline.Domain.Entities.Employees;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace Staffline.Infrastructure.Auth;

public sealed class JwtTokenGenerator(JwtOptions options) : IJwtTokenGenerator
{
    private readonly JsonWebTokenHandler _handler = new();

    public AccessToken Generate(Employee employee)
    {
        var expires = DateTime.UtcNow.AddMinutes(options.ExpiryMinutes);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, employee.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, employee.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        claims.AddRange(employee.Roles.Select(r =>
            new Claim("role", r.Role.ToString())));

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expires,
            Issuer = options.Issuer,
            Audience = options.Audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey)),
                SecurityAlgorithms.HmacSha256
            )
        };

        return new AccessToken(_handler.CreateToken(descriptor), expires);
    }
}