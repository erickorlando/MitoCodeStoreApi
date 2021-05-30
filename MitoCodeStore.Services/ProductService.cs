using MitoCodeStore.DataAccess.Repositories;
using MitoCodeStore.Dto;
using MitoCodeStore.Dto.Response;
using MitoCodeStore.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace MitoCodeStore.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductDtoResponse> SelectAllAsync(string filter, int page, int rows)
        {
            var response = new ProductDtoResponse();

            var tupla = await _repository.GetCollectionAsync(filter ?? string.Empty, page, rows);

            response.Products = tupla.collection
                .Select(x => new ProductDto
                {
                    ProductId = x.Id,
                    Category = x.CategoryName,
                    ProductName = x.Name,
                    UnitPrice = x.UnitPrice
                })
                .ToList();

            response.TotalPages = tupla.total;

            return response;
        }

        public async Task<ResponseDto<ProductSingleDtoResponse>> GetProductAsync(int id)
        {
            var response = new ResponseDto<ProductSingleDtoResponse>();
            var product = await _repository.GetItemAsync(id);

            if (product == null)
            {
                response.Success = false;
                return response;
            }

            response.Result = new ProductSingleDtoResponse
            {
                ProductId = product.Id,
                ProductName = product.Name,
                CategoryId = product.CategoryId,
                ProductPrice = product.UnitPrice,
                ProductEnabled = product.Enabled,
                ProductUrlImage = product.Picture
            };

            response.Success = true;

            return response;
        }

        public async Task<ProductSingleDtoResponse> CreateAsync(ProductSingleDtoResponse request)
        {


            var result = await _repository.CreateAsync(new Product
            {
                Name = request.ProductName,
                CategoryId = request.CategoryId,
                UnitPrice = request.ProductPrice,
                Enabled = request.ProductEnabled,
                Picture = request.ProductUrlImage
            });

            return new ProductSingleDtoResponse
            {
                ProductId = result,
                ProductName = request.ProductName,
                CategoryId = request.CategoryId,
                ProductPrice = request.ProductPrice,
                ProductEnabled = request.ProductEnabled,
                ProductUrlImage = request.ProductUrlImage
            };

        }

        public async Task UpdateAsync(int id, ProductSingleDtoResponse request)
        {
            await _repository.UpdateAsync(new Product
            {
                Id = id,
                Name = request.ProductName,
                CategoryId = request.CategoryId,
                UnitPrice = request.ProductPrice,
                Enabled = request.ProductEnabled,
                Picture = request.ProductUrlImage
            });
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}