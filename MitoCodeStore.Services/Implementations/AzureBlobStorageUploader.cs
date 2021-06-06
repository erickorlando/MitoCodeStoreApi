using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MitoCodeStore.Entities;
using MitoCodeStore.Services.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MitoCodeStore.Services.Implementations
{
    public class AzureBlobStorageUploader : IFileUploader
    {
        private readonly IOptions<AppSettings> _options;
        private readonly ILogger<AppSettings> _logger;

        public AzureBlobStorageUploader(IOptions<AppSettings> options, ILogger<AppSettings> logger)
        {
            _options = options;
            _logger = logger;
        }

        public async Task<string> UploadAsync(string base64String, string filename)
        {
            if (string.IsNullOrEmpty(base64String)) return string.Empty;

            var client = new BlobServiceClient(_options.Value.StorageConfiguration.Path);

            var container = client.GetBlobContainerClient("images");

            var blobClient = container.GetBlobClient(filename);

            using (var mem = new MemoryStream(Convert.FromBase64String(base64String)))
            {
                await blobClient.UploadAsync(mem, overwrite: true);

                return $"{_options.Value.StorageConfiguration.PublicUrl}{filename}";
            }
        }
    }
}