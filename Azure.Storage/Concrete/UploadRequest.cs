using System.IO;

namespace Azure.CustomStorage
{
    public class UploadRequest
    {
        public UploadRequest((string fileName, string container, Stream stream) request)
        {
            File = request.stream;
            FileName = request.fileName;
            ContainerName = request.container;
        }

        public string ContainerName { get; }
        public string FileName { get; }
        public Stream File { get; }
    }
}
