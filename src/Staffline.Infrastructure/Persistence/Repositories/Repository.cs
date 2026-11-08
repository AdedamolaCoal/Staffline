using Microsoft.EntityFrameworkCore;
using Staffline.Application.Common.Abstractions;
using Staffline.Domain.Common;

namespace Staffline.Infrastructure.Persistence.Repositories;

public class Repository<T>(AppDbContext db) : IRepository<T> where T : BaseEntity
{
    protected AppDbContext Db { get; } = db;
    protected DbSet<T> Set => Db.Set<T>();

    public virtual Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return Set.FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    public void Add(T entity) => Set.Add(entity);

    public void Remove(T entity) => Set.Remove(entity);
}