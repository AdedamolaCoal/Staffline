namespace Staffline.Application.Common.Contracts;

public sealed record PaginationMeta(
    int PageNumber,
    int PageSize,
    int TotalCount,
    int TotalPages,
    bool HasPrevious,
    bool HasNext
);