using Staffline.Domain.Common;

namespace Staffline.Application.Common.Abstractions;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default);
    void Add(T entity);
    void Remove(T entity);
}