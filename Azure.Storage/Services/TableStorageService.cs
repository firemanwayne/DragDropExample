using Azure.CustomStorage.Options;
using Microsoft.Extensions.Options;

namespace Azure.CustomStorage
{
    internal class TableStorageService : AzureServiceBase, ITableStorageService
    {
        public TableStorageService(IOptions<AzureStorageOptions> o) : base(o)
        {
        }
    }
}
