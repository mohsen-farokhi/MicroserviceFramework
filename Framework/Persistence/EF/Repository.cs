using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Persistence.EF
{
    public abstract class Repository<TEntity> :
        IRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        public Repository(DbContext databaseContext)
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

        public async Task<TEntity> AddAsync
            (TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(paramName: nameof(entity));
            }

            var result =
                await DbSet.AddAsync
                (entity: entity, cancellationToken: cancellationToken);

            return result.Entity;
        }

        public async Task AddRangeAsync
            (IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(paramName: nameof(entities));
            }

            await DbSet.AddRangeAsync
                (entities: entities, cancellationToken: cancellationToken);
        }

        public async Task RemoveAsync
            (TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(paramName: nameof(entity));
            }

            await Task.Run(() =>
            {
                DbSet.Remove(entity: entity);

                //var attachedEntity =
                //	DatabaseContext.Attach(entity: entity);

                //attachedEntity.State =
                //	Microsoft.EntityFrameworkCore.EntityState.Deleted;
            }, cancellationToken: cancellationToken);
        }

        public async Task<bool> RemoveByIdAsync
            (int id, CancellationToken cancellationToken = default)
        {
            TEntity entity =
                await FindAsync(id: id, cancellationToken: cancellationToken);

            if (entity == null)
            {
                return false;
            }

            await RemoveAsync
                (entity: entity, cancellationToken: cancellationToken);

            return true;
        }

        public async Task RemoveRangeAsync
            (IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(paramName: nameof(entities));
            }

            foreach (var entity in entities)
            {
                await RemoveAsync
                    (entity: entity, cancellationToken: cancellationToken);
            }
        }

        public async Task UpdateAsync
            (TEntity entity, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                var attachedEntity =
                    DatabaseContext.Attach(entity: entity);

                if (attachedEntity.State != EntityState.Modified)
                {
                    attachedEntity.State =
                        EntityState.Modified;
                }
            }, cancellationToken: cancellationToken);
        }

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
