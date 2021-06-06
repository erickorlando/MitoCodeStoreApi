using Microsoft.Extensions.Logging;
using MitoCodeStore.DataAccess.Repositories;
using MitoCodeStore.Dto.Request;
using MitoCodeStore.Dto.Response;
using MitoCodeStore.Entities;
using MitoCodeStore.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MitoCodeStore.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<IProductRepository> _logger;
        private readonly IFileUploader _fileUploader;

        public ProductService(IProductRepository repository, ILogger<IProductRepository> logger, IFileUploader fileUploader)
        {
            _repository = repository;
            _logger = logger;
            _fileUploader = fileUploader;
        }

        public async Task<ProductDtoResponse> GetCollectionAsync(BaseDtoRequest request)
        {
            var response = new ProductDtoResponse();

            var tupla = await _repository.GetCollectionAsync(request.Filter ?? string.Empty, request.Page, request.Rows);

            response.Collection = tupla.collection
                .Select(x => new ProductDto
                {
                    ProductId = x.Id,
                    Category = x.CategoryName,
                    ProductName = x.Name,
                    UnitPrice = x.UnitPrice
                })
                .ToList();

            response.TotalPages = MitoCodeStoreUtils.GetTotalPages(tupla.total, request.Rows);

            return response;
        }

        public async Task<ResponseDto<ProductDtoSingleResponse>> GetProductAsync(int id)
        {
            var response = new ResponseDto<ProductDtoSingleResponse>();
            try
            {
                var product = await _repository.GetItemAsync(id);

                if (product == null)
                {
                    response.Success = false;
                    return response;
                }

                response.Result = new ProductDtoSingleResponse
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    CategoryId = product.CategoryId,
                    ProductPrice = product.UnitPrice,
                    ProductEnabled = product.Enabled,
                    ProductUrlImage = product.Picture
                };

                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                response.Success = false;
            }

            return response;
        }

        public async Task<ResponseDto<int>> CreateAsync(ProductDtoRequest request)
        {
            var response = new ResponseDto<int>();

            try
            {
                var url = await _fileUploader.UploadAsync(request.ProductBase64Image, request.FileName);

                var result = await _repository.CreateAsync(new Product
                {
                    Name = request.ProductName,
                    CategoryId = request.CategoryId,
                    UnitPrice = request.ProductPrice,
                    Enabled = request.ProductEnabled,
                    Picture = url
                });

                response.Result = result;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                response.Success = false;
            }

            return response;

        }

        public async Task<ResponseDto<int>> UpdateAsync(int id, ProductDtoRequest request)
        {
            var response = new ResponseDto<int>();

            try
            {
                var url = request.ProductBase64Image;

                if (!string.IsNullOrEmpty(request.FileName))
                    url = await _fileUploader.UploadAsync(request.ProductBase64Image, request.FileName);

                await _repository.UpdateAsync(new Product
                {
                    Id = id,
                    Name = request.ProductName,
                    CategoryId = request.CategoryId,
                    UnitPrice = request.ProductPrice,
                    Enabled = request.ProductEnabled,
                    Picture = url
                });

                response.Result = id;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                response.Success = false;
            }

            return response;
        }

        public async Task<ResponseDto<int>> DeleteAsync(int id)
        {
            var response = new ResponseDto<int>();
            try
            {
                await _repository.DeleteAsync(id);
                response.Result = id;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                response.Success = false;
            }

            return response;
        }
    }
}