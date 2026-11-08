namespace Staffline.Application.Common.Contracts;

public sealed class ApiResponse<T>
{
    public bool Success => true;
    public T? Data { get; init; }
    public string? Message { get; init; }
    public PaginationMeta? Pagination { get; init; }
}

public static class ApiResponse
{
    public static ApiResponse<T> Ok<T>(
        T data,
        string? message = null
    ) => new()
    {
        Data = data,
        Message = message
    };

    public static ApiResponse<IReadOnlyList<T>> Paged<T>(
        PageResult<T> page
    ) => new()
    {
        Data = page.Items,
        Pagination = new PaginationMeta(
            page.PageNumber,
            page.PageSize,
            page.TotalCount,
            page.TotalPages,
            page.PageNumber > 1,
            page.PageNumber < page.TotalPages
        ),
    };
}