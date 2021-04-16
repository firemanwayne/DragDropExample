using Azure.CustomStorage;
using Azure.CustomStorage.Options;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RegisterServicesExtension
    {
        public static IServiceCollection AddAzureStorageServices(this IServiceCollection s, Action<AzureStorageOptions> o)
        {
            s.Configure(o);

            s.TryAddScoped<ITableStorageService>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<AzureStorageOptions>>();
                return new TableStorageService(options);
            });
            s.TryAddScoped<IBlobStorageService>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<AzureStorageOptions>>();
                return new BlobStorageService(options);
            });
            s.TryAddScoped<IQueueStorageService>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<AzureStorageOptions>>();
                return new QueueStorageService(options);
            });

            return s;
        }
    }
}
