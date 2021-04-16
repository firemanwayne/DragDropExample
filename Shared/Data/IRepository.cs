using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DragDropExample.Shared.Data
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<T> SaveAsync(T Entity);

        IAsyncEnumerable<T> GetAsync(Expression<Func<T, bool>> Predicate = null);

        Task<T> SearchAsync(string Id);
    }
}
