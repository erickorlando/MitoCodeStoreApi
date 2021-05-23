﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MitoCodeStore.Entities;

namespace MitoCodeStore.DataAccess
{
    public interface ICustomerRepository
    {
        Task<(ICollection<Customer> collection, int total)> GetCollectionAsync(string filter, int page, int rows);

        Task<Customer> GetItemAsync(int id);

        Task<Customer> CreateAsync(Customer entity);

        Task UpdateAsync(int id, Customer entity);

        Task DeleteAsync(int id);
    }
}