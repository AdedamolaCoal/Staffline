using Microsoft.EntityFrameworkCore;
using Staffline.Application.Common.Contracts;

namespace Staffline.Infrastructure.Persistence;

public static class PagedQueryExtensions
{
    public static async Task<PageResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        PageRequest page,
        CancellationToken ct = default
    )
    {
        var total = await query.CountAsync(ct);
        var items = await query.Skip(page.Skip).Take(page.PageSize).ToListAsync(ct);
        return new PageResult<T>(
            items,
            page.PageNumber,
            page.PageSize,
            total
        );
    }
}