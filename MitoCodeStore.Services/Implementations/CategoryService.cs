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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly ILogger<ICategoryRepository> _logger;

        public CategoryService(ICategoryRepository repository, ILogger<ICategoryRepository> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<CategoryDtoResponse> GetCollectionAsync(BaseDtoRequest request)
        {
            var response = new CategoryDtoResponse();

            var tupla = await _repository.GetCollectionAsync(request.Filter ?? string.Empty,
                request.Page, request.Rows);

            response.Collection = tupla.collection
                .Select(p => new CategoryDtoSingleResponse
                {
                    CategoryId = p.Id,
                    CategoryName = p.Name,
                    CategoryDescription = p.Description
                }).ToList();

            response.TotalPages = MitoCodeStoreUtils.GetTotalPages(tupla.total, request.Rows);

            return response;
        }

        public async Task<ResponseDto<CategoryDtoSingleResponse>> GetAsync(int id)
        {
            var response = new ResponseDto<CategoryDtoSingleResponse>();

            try
            {
                var category = await _repository.GetItemAsync(id);
                response.Result = new CategoryDtoSingleResponse
                {
                    CategoryId = category.Id,
                    CategoryDescription = category.Description,
                    CategoryName = category.Name
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

        public async Task<ResponseDto<int>> CreateAsync(CategoryDtoRequest request)
        {
            var response = new ResponseDto<int>();
            try
            {
                response.Result = await _repository.CreateAsync(new Category
                {
                    Name = request.Name,
                    Description = request.Description
                });
                
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                response.Success = false;
            }
            return response;
        }

        public async Task<ResponseDto<int>> UpdateAsync(int id, CategoryDtoRequest request)
        {
            var response = new ResponseDto<int>();

            try
            {
                await _repository.UpdateAsync(new Category
                {
                    Id = id,
                    Name = request.Name,
                    Description = request.Description
                });

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