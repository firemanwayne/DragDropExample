using Microsoft.Extensions.Options;
using RedisCache;
using RedisCache.Stores;
using StackExchange.Redis;
using System;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RegistryExtensions
    {
        public static IServiceCollection AddAzureRedisCache(this IServiceCollection services, Action<RedisCacheConfiguration> opts)
        {
            services.Configure(opts);

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var redisConfig = sp.GetRequiredService<IOptions<RedisCacheConfiguration>>();

                var redisOptsValue = redisConfig.Value;
                var connectionString = redisOptsValue
                .Settings
                .FirstOrDefault()
                .ConnectionString;

                return ConnectionMultiplexer.Connect(connectionString);
            });

            services.AddSingleton<ICacheStore>(sp => 
            {
                var connection = sp.GetRequiredService<IConnectionMultiplexer>();

                return new CacheStore(connection);
            });

            return services;
        }
    }
}
