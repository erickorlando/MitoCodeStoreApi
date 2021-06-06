using System.Collections.Generic;
using System.Threading.Tasks;
using MitoCodeStore.Entities;

namespace MitoCodeStore.DataAccess.Repositories
{
    public interface IPaymentMethodRepository
    {
        Task<ICollection<PaymentMethod>> GetCollectionAsync();
        Task<PaymentMethod> GetItemAsync(int id);
        Task<int> CreateAsync(PaymentMethod entity);
        Task UpdateAsync(PaymentMethod entity);
        Task DeleteAsync(int id);
    }
}