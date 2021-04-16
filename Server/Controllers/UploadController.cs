using Azure.CustomStorage;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DragDropExample.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        readonly IBlobStorageService BlobService;

        public UploadController(IBlobStorageService BlobService)
        {
            this.BlobService = BlobService;
        }

        [HttpPost("{container}")]
        public async Task<IActionResult> Upload(string container)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files[0];

                if (file.Length > 0)
                {
                    var blob = await BlobService.Upload(
                        new UploadRequest((file.Name, container, file.OpenReadStream())));
                    
                    return Ok(blob.Uri.AbsoluteUri);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
