using MitoCodeStore.Dto.Request;
using MitoCodeStore.Dto.Response;
using System.Threading.Tasks;

namespace MitoCodeStore.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDtoResponse> GetCollectionAsync(BaseDtoRequest request);
        Task<ResponseDto<ProductDtoSingleResponse>> GetProductAsync(int id);
        Task<ResponseDto<int>> CreateAsync(ProductDtoRequest request);
        Task<ResponseDto<int>> UpdateAsync(int id, ProductDtoRequest request);
        Task<ResponseDto<int>> DeleteAsync(int id);
    }
}