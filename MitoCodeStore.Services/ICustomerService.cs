using System.Collections.Generic;
using System.Threading.Tasks;
using MitoCodeStore.Dto;
using MitoCodeStore.Dto.Response;

namespace MitoCodeStore.Services
{
    public interface ICustomerService
    {
        Task<CustomerDtoResponse> GetCollectionAsync(string filter, int page, int rows);
        Task<ResponseDto<CustomerDto>> GetCustomerAsync(int id);

        Task<CustomerDto> CreateAsync(CustomerDto request);

        Task UpdateAsync(int id, CustomerDto request);

        Task DeleteAsync(int id);
    }
}