using System.Threading.Tasks;
using MitoCodeStore.Dto.Request;
using MitoCodeStore.Dto.Response;

namespace MitoCodeStore.Services.Interfaces
{
    public interface IUserService
    {
        Task<RegisterUserDtoResponse> RegisterAsync(RegisterUserDtoRequest request);

        Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request);
    }
}