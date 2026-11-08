namespace Staffline.Application.Features.Auth.Contracts;

public sealed record AccessToken(
    string Token,
    DateTime ExpiresAtUtc
);