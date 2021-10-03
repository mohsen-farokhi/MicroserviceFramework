using Framework.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Persistence
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        Task<T> AddAsync
            (T entity, CancellationToken cancellationToken = default);

        Task AddRangeAsync
            (IEnumerable<T>
            entities, CancellationToken cancellationToken = default);

        Task UpdateAsync
            (T entity, CancellationToken cancellationToken = default);

        Task RemoveAsync
            (T entity, CancellationToken cancellationToken = default);

        Task RemoveRangeAsync
            (IEnumerable<T> entities,
            CancellationToken cancellationToken = default);

        Task<bool> RemoveByIdAsync
            (int id, CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> Find
            (System.Linq.Expressions.Expression<System.Func<T, bool>> predicate,
            CancellationToken cancellationToken = default);

        Task<T> FindAsync
            (int id, CancellationToken cancellationToken = default);
    }
}
