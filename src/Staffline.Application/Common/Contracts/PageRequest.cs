namespace Staffline.Application.Common.Contracts;

public class PageRequest
{
    public const int MaxPageSize = 100;
    private int _pageNumber = 1;
    private int _pageSize = 20;

    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value < 1 ? 1 : value;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value < 1 ? 20 : Math.Min(value, MaxPageSize);
    }

    public int Skip => (PageNumber - 1) * PageSize;
}

public sealed record PageResult<T>(
    IReadOnlyList<T> Items,
    int PageNumber,
    int PageSize,
    int TotalCount
)
{
    public int TotalPages
        => PageSize == 0
            ? 0
            : (int)Math.Ceiling(TotalCount / (double)PageSize);
}