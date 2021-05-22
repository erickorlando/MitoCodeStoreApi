using System.Collections.Generic;
using System.Threading.Tasks;
using MitoCodeStore.Dto;

namespace MitoCodeStore.Services
{
    public interface ICustomerService
    {
        Task<ICollection<CustomerDto>> GetCollectionAsync(string filter);
        Task<ResponseDto<CustomerDto>> GetCustomerAsync(int id);

        Task CreateAsync(CustomerDto request);

        Task UpdateAsync(int id, CustomerDto request);

        Task DeleteAsync(int id);
    }
}