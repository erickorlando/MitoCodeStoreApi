using System.Collections.Generic;
using System.Threading.Tasks;
using MitoCodeStore.Entities;

namespace MitoCodeStore.DataAccess.Repositories
{
    public interface ICustomerRepository
    {
        Task<(ICollection<Customer> collection, int total)> GetCollectionAsync(string filter, int page, int rows);

        Task<Customer> GetItemAsync(int id);

        Task<Customer> GetItemByEmailAsync(string email);

        Task<int> CreateAsync(Customer entity);

        Task UpdateAsync(Customer entity);

        Task DeleteAsync(int id);
    }
}