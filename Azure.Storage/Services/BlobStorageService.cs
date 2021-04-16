using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.CustomStorage.Options;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Console;
using System;

namespace Azure.CustomStorage
{
    internal class BlobStorageService : AzureServiceBase, IBlobStorageService
    {
        readonly BlobServiceClient serviceClient;        

        public BlobStorageService(
            IOptions<AzureStorageOptions> o) : base(o)
        {            
            serviceClient = new(Options.ConnectionString);
        }

        public async Task<BlobContainerClient> CreateContainer(string container)
        {
            var response = await serviceClient.CreateBlobContainerAsync(container);
            var rawResponse = response.GetRawResponse();

            WriteLine($"Azure Storage HttpStatus: {rawResponse.Status}");

            return response.Value;
        }

        public Task DeleteContainer(string container) => serviceClient.DeleteBlobContainerAsync(container);        



        public Task<BlobClient> GetBlob((string container, string fileName) b)
        {
            var containerClient = serviceClient.GetBlobContainerClient(b.container);

            if (containerClient != null)
                return Task.FromResult(containerClient.GetBlobClient(b.fileName));

            return default;
        }
        public async IAsyncEnumerable<BlobItem> GetBlobs(string container)
        {
            var containerClient = serviceClient.GetBlobContainerClient(container);

            if (containerClient != null)
                await foreach (var blobItem in containerClient.GetBlobsAsync())
                {
                    WriteLine("\t" + blobItem.Name);
                    yield return blobItem;
                }
        }

        public async Task<BlobClient> Upload(UploadRequest request) => await serviceClient.UploadAsync(request);    
        
        public IAsyncEnumerable<BlobContainerItem> GetContainers() => serviceClient.GetBlobContainersAsync();        
    }

    static class ClientExtensions
    {
        public static async Task<BlobClient> UploadAsync(this BlobServiceClient client, UploadRequest request)
        {
            var containerClient = client.GetBlobContainerClient(request.ContainerName);

            if (containerClient != null)
            {
                var createResponse = await containerClient.CreateIfNotExistsAsync();

                if (createResponse != null)
                    await CreateContainer(containerClient, createResponse);

                var blobClient = containerClient.GetBlobClient(request.FileName);

                await blobClient.UploadAsync(request.File, true);

                return blobClient;
            }

            return default;
        }

        static async Task<Response> CreateContainer(BlobContainerClient containerClient, Response<BlobContainerInfo> response)
        {
            var rawResponse = response.GetRawResponse();

            if (rawResponse.Status == 201)
                await containerClient.SetAccessPolicyAsync(PublicAccessType.Blob);

            return rawResponse;
        }
    }
}
