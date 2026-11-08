namespace Staffline.Application.Features.Auth.Contracts;

public sealed record LoginRequest(
    string Email,
    string Password
);