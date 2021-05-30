using System.Threading.Tasks;
using MitoCodeStore.Entities;

namespace MitoCodeStore.DataAccess.Repositories
{
    public interface IPaymentMethodRepository
    {
        Task<PaymentMethod> GetItemAsync(int id);
        Task<int> CreateAsync(PaymentMethod entity);
        Task UpdateAsync(PaymentMethod entity);
        Task DeleteAsync(int id);
    }
}