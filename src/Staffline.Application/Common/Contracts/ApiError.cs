namespace Staffline.Application.Common.Contracts;

public sealed record ApiError(
    string Code,
    string Message,
    IReadOnlyDictionary<string, string[]>? Details = null
);