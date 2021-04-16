using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace StackExchange.Redis
{
    public static class DatabaseExtensions
    {
        static readonly Func<TimeSpan, TimeSpan> GetTimeToLive = (Expiration) => Expiration.Subtract(DateTime.UtcNow.TimeOfDay);

        public static async Task<T> GetOrAddAsync<T>(this IDatabase d, RedisKey Key, Func<Task<T>> DataTask, DateTimeOffset? Expiration, bool ShowConsole = false)
        {
            object NewDataToBeCached = null;

            if (d == null)
                throw new ArgumentNullException(nameof(d));

            T CachedData = await GetAsync<T>(d, Key);

            if (CachedData == null)
            {
                NewDataToBeCached = await DataTask.Invoke();

                if (NewDataToBeCached == null)
                    throw new InvalidOperationException($"The following Func<T>: {nameof(DataTask)}\nOf type: {typeof(T).Name}\n returned no data. Data provider Task cannot return null");

                if (NewDataToBeCached is IEnumerable<T> e)
                {
                    if (e.Any())
                        await d.SetAsync(Key, NewDataToBeCached, Expiration);
                }
                else
                    await d.SetAsync(Key, NewDataToBeCached, Expiration);

                if (ShowConsole)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(NewDataToBeCached);
                }
            }

            return CachedData ?? (T)NewDataToBeCached ?? default;
        }
        public static async Task<T> GetAsync<T>(this IDatabase d, RedisKey key, bool ShowConsole = false)
        {
            var valueJson = await d.StringGetAsync(key);

            if (!string.IsNullOrEmpty(valueJson))
            {
                var value = JsonSerializer.Deserialize<T>(valueJson);

                if (ShowConsole)
                {
                    Console.WriteLine($"<<<<<<<<<<<  Deserialize Type: {typeof(T).Name}   >>>>>>>>>>>");

                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(valueJson);
                }
                return value ?? default;
            }
            return default;
        }
        public static async Task<bool> SetAsync<T>(this IDatabase d, RedisKey Key, T Entry, DateTimeOffset? Expiration)
        {
            if (Entry != null)
            {
                var valueJson = JsonSerializer.Serialize(Entry);

                if (Expiration.HasValue)
                {
                    var ttl = GetTimeToLive(Expiration.Value.TimeOfDay);

                    return await d.StringSetAsync(Key, valueJson, ttl);
                }
                else
                    return await d.StringSetAsync(Key, valueJson);
            }
            else
                return false;
        }
    }
}
