using Microsoft.EntityFrameworkCore;
using MitoCodeStore.Entities;
using MitoCodeStore.Entities.Complex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MitoCodeStore.DataAccess.Repositories
{
    public class ProductRepository : RepositoryContextBase<Product>, IProductRepository
    {
        public ProductRepository(MitoCodeStoreDbContext context) : base(context)
        {
        }


        public async Task<(ICollection<ProductInfo> collection, int total)> GetCollectionAsync(string filter, int page, int rows)
        {
            var tupla = await List(p => p.Name.Contains(filter), page, rows);

            var collection = tupla.collection
               
                .ToList();

            return (collection, tupla.total);
        }

        public async Task<(ICollection<ProductInfo> collection, int total)> List(Expression<Func<Product, bool>> predicate, int page, int rows)
        {
            var collection = await Context.Set<Product>()
                .Where(predicate).OrderBy(p => p.Id)
                .Select(p => new ProductInfo
                {
                    Id = p.Id,
                    Name = p.Name,
                    CategoryName = p.Category.Name,
                    UnitPrice = p.UnitPrice,
                    ImageUrl = p.Picture
                })
                .AsNoTracking()
                .Skip((page - 1) * rows)
                .Take(rows)
                .ToListAsync();

            var totalCount = await Context.Set<Product>()
                .Where(predicate)
                .AsNoTracking()
                .CountAsync();

            return (collection.ToList(), totalCount);
        }

        public async Task<Product> GetItemAsync(int id)
        {
            return await Select(id);
        }

        public async Task<int> CreateAsync(Product entity)
        {
            return await Context.Insert(entity);
        }

        public async Task UpdateAsync(Product entity)
        {
            await Context.UpdateEntity(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await Context.Delete(new Product
            {
                Id = id
            });
        }
    }
}