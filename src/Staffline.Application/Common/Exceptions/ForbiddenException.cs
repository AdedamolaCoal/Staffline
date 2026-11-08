namespace Staffline.Application.Common.Exceptions;

public sealed class ForbiddenException(string message = "You do not have sufficient permission to perform this action.")
    : AppException(message, "forbidden");