using Azure.CustomStorage.Options;
using Microsoft.Extensions.Options;

namespace Azure.CustomStorage
{
    internal class QueueStorageService : AzureServiceBase, IQueueStorageService
    {
        public QueueStorageService(IOptions<AzureStorageOptions> o) : base(o)
        {
        }
    }
}
