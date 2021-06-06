using System.Threading.Tasks;
using MitoCodeStore.Dto;
using MitoCodeStore.Dto.Request;
using MitoCodeStore.Dto.Response;

namespace MitoCodeStore.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDtoResponse> GetCollectionAsync(BaseDtoRequest request);

        Task<ResponseDto<CustomerDtoSingleResponse>> GetCustomerAsync(int id);

        Task<ResponseDto<int>> CreateAsync(CustomerDtoRequest request);

        Task<ResponseDto<int>> UpdateAsync(int id, CustomerDtoRequest request);

        Task<ResponseDto<int>> DeleteAsync(int id);
    }
}