using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Azure.CustomStorage
{
    public interface IBlobStorageService
    {
        Task<BlobClient> Upload(UploadRequest request);
        IAsyncEnumerable<BlobItem> GetBlobs(string container);

        Task<BlobClient> GetBlob((string container, string fileName) b);


        Task<BlobContainerClient> CreateContainer(string container);
        Task DeleteContainer(string container);
        IAsyncEnumerable<BlobContainerItem> GetContainers();
    }
}
