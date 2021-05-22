using Microsoft.EntityFrameworkCore;
using MitoCodeStore.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MitoCodeStore.DataAccess
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MitoCodeStoreDbContext _context;

        public CustomerRepository(MitoCodeStoreDbContext context)
        {
            _context = context;
        }


        public async Task<ICollection<Customer>> GetCollectionAsync(string filter)
        {
            var collection = await _context.Customers
                .Where(p => p.Name.Contains(filter))
                .AsNoTracking()
                .ToListAsync();

            return collection;
        }

        public async Task<Customer> GetItemAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task CreateAsync(Customer entity)
        {
            await _context.Set<Customer>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Customer entity)
        {
            var customer = await _context.Customers
                .SingleOrDefaultAsync(p => p.Id == id);

            customer.Name = entity.Name;
            customer.BirthDate = entity.BirthDate;
            customer.NumberId = entity.NumberId;

            //_context.Customers.Attach(customer);
            _context.Set<Customer>().Attach(customer);
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await _context.Customers
                .SingleOrDefaultAsync(p => p.Id == id);

            if (customer != null)
            {
                _context.Set<Customer>().Remove(customer);
                _context.Entry(customer).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }
    }
}
