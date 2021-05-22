using System.Collections.Generic;
using System.Threading.Tasks;
using MitoCodeStore.Entities;

namespace MitoCodeStore.DataAccess
{
    public interface ICustomerRepository
    {
        Task<ICollection<Customer>> GetCollectionAsync(string filter);

        Task<Customer> GetItemAsync(int id);

        Task CreateAsync(Customer entity);

        Task UpdateAsync(int id, Customer entity);

        Task DeleteAsync(int id);
    }
}