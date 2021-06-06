using System.Threading.Tasks;

namespace MitoCodeStore.Services.Interfaces
{
    public interface IFileUploader
    {
        Task<string> UploadAsync(string base64String, string filename);
    }
}