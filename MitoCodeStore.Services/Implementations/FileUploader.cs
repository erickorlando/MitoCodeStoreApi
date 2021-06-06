using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MitoCodeStore.Entities;
using MitoCodeStore.Services.Interfaces;

namespace MitoCodeStore.Services.Implementations
{
    public class FileUploader : IFileUploader
    {
        private readonly IOptions<AppSettings> _options;
        private readonly ILogger<AppSettings> _logger;

        public FileUploader(IOptions<AppSettings> options, ILogger<AppSettings> logger)
        {
            _options = options;
            _logger = logger;
        }

        public async Task<string> UploadAsync(string base64String, string filename)
        {
            var bytes = Convert.FromBase64String(base64String);

            try
            {
                var path = Path.Combine(_options.Value.StorageConfiguration.Path, filename);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await fileStream.WriteAsync(bytes, 0, bytes.Length);
                }

                return $"{_options.Value.StorageConfiguration.PublicUrl}{filename}";
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return string.Empty;
            }
        }
    }
}