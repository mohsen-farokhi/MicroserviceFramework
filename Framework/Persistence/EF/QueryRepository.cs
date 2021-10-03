using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Persistence.EF
{
    public abstract class QueryRepository<TEntity> :
        IQueryRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        public QueryRepository
            (DbContext databaseContext) : base()
        {
            DatabaseContext =
                databaseContext ??
                throw new ArgumentNullException(paramName: nameof(databaseContext));

            DbSet = DatabaseContext.Set<TEntity>();
        }

        // **********
        protected DbSet<TEntity> DbSet { get; }
        // **********

        // **********
        protected DbContext DatabaseContext { get; }
        // **********

        public async Task<IEnumerable<TEntity>> Find
            (System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            var result =
                await DbSet
                .Where(predicate: predicate)
                .ToListAsync(cancellationToken: cancellationToken);

            return result;
        }

        public async Task<TEntity> FindAsync
            (int id, CancellationToken cancellationToken = default)
        {
            var result =
                await DbSet
                .FindAsync(keyValues: new object[] { id },
                cancellationToken: cancellationToken);

            return result;
        }
    }
}
