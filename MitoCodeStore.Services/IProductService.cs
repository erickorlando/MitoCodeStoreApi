using System.Threading.Tasks;
using MitoCodeStore.Dto;
using MitoCodeStore.Dto.Response;

namespace MitoCodeStore.Services
{
    public interface IProductService
    {
        Task<ProductDtoResponse> SelectAllAsync(string filter, int page, int rows);
        Task<ResponseDto<ProductSingleDtoResponse>> GetProductAsync(int id);
        Task<ProductSingleDtoResponse> CreateAsync(ProductSingleDtoResponse request);
        Task UpdateAsync(int id, ProductSingleDtoResponse request);
        Task DeleteAsync(int id);
    }
}