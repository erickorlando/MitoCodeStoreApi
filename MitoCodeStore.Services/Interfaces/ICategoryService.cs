using System.Threading.Tasks;
using MitoCodeStore.Dto.Request;
using MitoCodeStore.Dto.Response;

namespace MitoCodeStore.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDtoResponse> GetCollectionAsync(BaseDtoRequest request);
        
        Task<ResponseDto<CategoryDtoSingleResponse>> GetAsync(int id);

        Task<ResponseDto<int>> CreateAsync(CategoryDtoRequest request);

        Task<ResponseDto<int>> UpdateAsync(int id, CategoryDtoRequest request);

        Task<ResponseDto<int>> DeleteAsync(int id);
    }
}