using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Staffline.Application.Common.Contracts;
using Staffline.Application.Common.Exceptions;

namespace Staffline.Api.Common;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
        CancellationToken ct)
    {
        var (status, code, message, details) = Map(exception);

        if (status >= 500) logger.LogError(exception, "Unhandled exception");
        else logger.LogWarning("Request rejected: {Code} ({Status}).", code, status);

        context.Response.StatusCode = status;
        await context.Response.WriteAsJsonAsync(
            ApiErrorResponse.Create(
                code,
                message,
                context.TraceIdentifier,
                details
            )
        );
        return true;
    }

    private static (
        int Dtatus,
        string Code,
        string Message,
        IReadOnlyDictionary<string, string[]>? Details
        ) Map(Exception ex) =>
        ex switch
        {
            ValidationException v => (400, v.Code, v.Message, v.Errors),
            UnauthorizedException v => (401, v.Code, v.Message, null),
            ForbiddenException v => (403, v.Code, v.Message, null),
            NotFoundException v => (404, v.Code, v.Message, null),
            ConflictException v => (409, v.Code, v.Message, null),

            DbUpdateConcurrencyException v => (409, "concurrency_conflict",
                "This record was changed by another request. Reload and try again.", null),
            DbUpdateException { InnerException: SqlException { Number: 2601 or 2627 } }
                => (409, "duplicate", "A record with the same unique value already exists.", null),

            _ => (500, "server_error", "An unexpected error occurred.", null)
        };
}