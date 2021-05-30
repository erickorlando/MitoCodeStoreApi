using System.Collections.Generic;
using System.Threading.Tasks;
using MitoCodeStore.Entities;

namespace MitoCodeStore.DataAccess.Repositories
{
    public interface ICategoryRepository
    {
        Task<(ICollection<Category> collection, int total)> GetCollectionAsync(string filter, int page, int rows);
        Task<Category> GetItemAsync(int id);
        Task<int> CreateAsync(Category entity);
        Task UpdateAsync(Category entity);
        Task DeleteAsync(int id);
    }
}