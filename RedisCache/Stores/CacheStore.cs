using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace RedisCache.Stores
{
    public class CacheStore : ICacheStore
    {
        readonly IDatabase DataBase;

        public CacheStore(IConnectionMultiplexer Connection)
        {
            DataBase = Connection.GetDatabase();

            Connection.InternalError += (o, a) =>
            {
                Console.WriteLine($"{a.Exception.Message}");
            };

            Console.WriteLine($"{Connection.IsConnected}");
        }

        public async Task<T> GetOrAddAsync<T>(RedisKey Key, Func<Task<T>> DataTask, Action<Exception> ExceptionHandler = null)
        {
            try
            {
                return await DataBase.GetOrAddAsync(
                    Key: Key,
                    DataTask: DataTask,
                    Expiration: null);
            }
            catch (Exception ex)
            {
                if (ExceptionHandler != null)
                    ExceptionHandler.Invoke(ex);

                throw;
            }
        }
    }
}
