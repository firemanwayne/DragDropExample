using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DragDropExample.Shared.Data
{
    public class EntityRepository<T> : IRepository<T> where T : EntityBase
    {
        readonly DbSet<T> Dbset;
        readonly DataContext context;

        public EntityRepository(DataContext context)
        {
            this.context = context;
            Dbset = context.Set<T>();
        }

        public IAsyncEnumerable<T> GetAsync(Expression<Func<T, bool>> Predicate = null)
        {
            if (Predicate == null)
                return Dbset.AsAsyncEnumerable();

            else
                return Dbset.Where(Predicate).AsAsyncEnumerable();
        }

        public Task<T> SaveAsync(T Entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> SearchAsync(string Id)
        {
            throw new NotImplementedException();
        }
    }

    public static class TransactionExtensions
    {
        public static async Task<bool> Commit<T>(this DataContext ctx, CommitDelegate<T> commitdelegate) where T : EntityBase
        {
            var trans = ctx
                .Database
                .BeginTransaction();

            var savePoint = Guid
                .NewGuid()
                .ToString();

            trans.CreateSavepoint(savePoint);

            foreach (var t in commitdelegate())
                await t();

            await trans.CommitAsync();

            await trans.ReleaseSavepointAsync(savePoint);

            return true;
        }
    }

    public delegate IEnumerable<Func<Task<T>>> CommitDelegate<T>();
}
