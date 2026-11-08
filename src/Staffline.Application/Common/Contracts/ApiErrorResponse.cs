namespace Staffline.Application.Common.Contracts;

public sealed class ApiErrorResponse
{
    public bool Success => false;
    public required ApiError Error { get; init; }
    public string? TraceId { get; init; }

    public static ApiErrorResponse Create(
        string code,
        string message,
        string? traceId,
        IReadOnlyDictionary<string, string[]>? details = null
    ) => new()
    {
        Error = new ApiError(
            code,
            message,
            details
        ),
        TraceId = traceId
    };
}