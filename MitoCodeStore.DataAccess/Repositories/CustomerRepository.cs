using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MitoCodeStore.Entities;

namespace MitoCodeStore.DataAccess.Repositories
{
    public class CustomerRepository : RepositoryContextBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(MitoCodeStoreDbContext context)
        : base(context)
        {

        }

        public async Task<(ICollection<Customer> collection, int total)> GetCollectionAsync(string filter, int page, int rows)
        {
            return await ListCollection(p => p.Name.Contains(filter), page, rows);
        }

        public async Task<Customer> GetItemAsync(int id)
        {
            return await Select(id);
        }

        public async Task<Customer> GetItemByEmailAsync(string email)
        {
            return await Context.Set<Customer>()
                .Where(x => x.Email.Equals(email))
                .SingleOrDefaultAsync();
        }

        public async Task<int> CreateAsync(Customer entity)
        {
            return await Context.Insert(entity);
        }

        public async Task UpdateAsync(Customer entity)
        {
            await Context.UpdateEntity(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await Context.Delete(new Customer
            {
                Id = id
            });
        }
    }
}
