using MitoCodeStore.Entities;
using System.Threading.Tasks;

namespace MitoCodeStore.DataAccess.Repositories
{
    public class PaymentMethodRepository : RepositoryContextBase<PaymentMethod>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(MitoCodeStoreDbContext context) : base(context)
        {
        }

        public async Task<PaymentMethod> GetItemAsync(int id)
        {
            return await Select(id);
        }

        public async Task<int> CreateAsync(PaymentMethod entity)
        {
            return await Context.Insert(entity);
        }

        public async Task UpdateAsync(PaymentMethod entity)
        {
            await Context.UpdateEntity(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await Context.Delete(new PaymentMethod
            {
                Id = id
            });
        }
    }
}