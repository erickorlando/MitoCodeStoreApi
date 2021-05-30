using MitoCodeStore.Entities;
using MitoCodeStore.Entities.Complex;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MitoCodeStore.DataAccess.Repositories
{
    public interface IProductRepository
    {
        Task<(ICollection<ProductInfo> collection, int total)> GetCollectionAsync(string filter, int page, int rows);
        Task<Product> GetItemAsync(int id);
        Task<int> CreateAsync(Product entity);
        Task UpdateAsync(Product entity);
        Task DeleteAsync(int id);
    }
}