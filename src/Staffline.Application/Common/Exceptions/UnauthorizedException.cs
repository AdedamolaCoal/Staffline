namespace Staffline.Application.Common.Exceptions;

public sealed class UnauthorizedException(string message = "Authentication failed")
    : AppException(message, "unauthorized");