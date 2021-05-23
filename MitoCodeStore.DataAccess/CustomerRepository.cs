﻿using Microsoft.EntityFrameworkCore;
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


        public async Task<(ICollection<Customer> collection, int total)> GetCollectionAsync(string filter, int page, int rows)
        {

            var query = _context.Customers
                .Where(p => p.Name.Contains(filter));

            var collection = await query.OrderBy(p => p.Id)
                .Select(p => new
                {
                    Customer = p,
                    Total = query.Select(x => x).Count()
                })
            .AsNoTracking()
            .Skip((page - 1) * rows)
            .Take(rows)
            .ToListAsync();

            return (collection.Select(y => y.Customer).ToList(),
                    collection.First().Total);
        }

        public async Task<Customer> GetItemAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<Customer> CreateAsync(Customer entity)
        {
            await _context.Set<Customer>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
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
