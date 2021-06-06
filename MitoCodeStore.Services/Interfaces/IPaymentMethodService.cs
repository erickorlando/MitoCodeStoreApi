using System.Threading.Tasks;
using MitoCodeStore.Dto.Request;
using MitoCodeStore.Dto.Response;

namespace MitoCodeStore.Services.Interfaces
{
    public interface IPaymentMethodService
    {
        Task<PaymentMethodDtoResponse> GetCollectionAsync();

        Task<ResponseDto<PaymentMethodDtoSingleResponse>> GetItemAsync(int id);

        Task<ResponseDto<int>> CreateAsync(PaymentMethodDtoRequest request);

        Task<ResponseDto<int>> UpdateAsync(int id, PaymentMethodDtoRequest request);

        Task<ResponseDto<int>> DeleteAsync(int id);
    }
}