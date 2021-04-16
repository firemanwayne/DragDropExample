using Azure.CustomStorage.Options;
using Microsoft.Extensions.Options;

namespace Azure.CustomStorage
{
    public abstract class AzureServiceBase
    {
        protected AzureStorageOptions Options { get; }

        protected AzureServiceBase(IOptions<AzureStorageOptions> o)
        {
            Options = o.Value;
        }
    }
}
