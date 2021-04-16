using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace RedisCache.Stores
{
    public interface ICacheStore
    {
        Task<T> GetOrAddAsync<T>(RedisKey Key, Func<Task<T>> DataTask, Action<Exception> ExceptionHandler = null);
    }
}
