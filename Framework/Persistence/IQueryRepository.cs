using Framework.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Persistence
{
    public interface IQueryRepository<T> where T : IAggregateRoot
    {
        Task<IEnumerable<T>> Find
            (System.Linq.Expressions.Expression<System.Func<T, bool>> predicate,
            CancellationToken cancellationToken = default);

        Task<T> FindAsync
            (int id, CancellationToken cancellationToken = default);
    }
}
