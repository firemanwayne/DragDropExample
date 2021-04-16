using Azure.CustomStorage;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DragDropExample.Server.Controllers
{
    [Route("api/azure")]
    [ApiController]
    public class AzureStorageController : Controller
    {
        readonly IBlobStorageService blobService;

        public AzureStorageController(IBlobStorageService blobService)
        {
            this.blobService = blobService;
        }

        [HttpGet("containers")]
        public async IAsyncEnumerable<BlobContainerItem> Containers()
        {
            await foreach (var c in blobService.GetContainers())
                yield return c;
        }

        [HttpGet("{container}/blobs")]
        public async IAsyncEnumerable<BlobItem> Blobs(string container)
        {
            if (string.IsNullOrEmpty(container))
                yield return default;

            else
                await foreach (var b in blobService.GetBlobs(container))
                    yield return b;
        }
    }
}
