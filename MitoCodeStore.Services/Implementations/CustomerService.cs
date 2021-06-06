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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CustomerDtoRequest> _logger;

        public CustomerService(ICustomerRepository repository, ILogger<CustomerDtoRequest> logger)
        {
            _repository = repository;
            _logger = logger;
        }


        public async Task<CustomerDtoResponse> GetCollectionAsync(BaseDtoRequest request)
        {
            var response = new CustomerDtoResponse();

            var tuple = await _repository
                .GetCollectionAsync(request.Filter ?? string.Empty, request.Page, request.Rows);

            response.Collection = tuple.collection
                .Select(p => new CustomerDtoSingleResponse()
                {
                    CustomerId = p.Id,
                    CustomerName = p.Name,
                    CustomerBirth = p.BirthDate.ToShortDateString(),
                    CustomerNumberId = p.NumberId
                })
                .ToList();

            response.TotalPages = MitoCodeStoreUtils.GetTotalPages(tuple.total, request.Rows);

            return response;

        }

        public async Task<ResponseDto<CustomerDtoSingleResponse>> GetCustomerAsync(int id)
        {
            var response = new ResponseDto<CustomerDtoSingleResponse>();
            try
            {
                var customer = await _repository.GetItemAsync(id);

                if (customer == null)
                {
                    response.Success = false;
                    return response;
                }

                response.Result = new CustomerDtoSingleResponse
                {
                    CustomerId = customer.Id,
                    CustomerName = customer.Name,
                    CustomerBirth = customer.BirthDate.ToString(Constants.DateFormat),
                    CustomerNumberId = customer.NumberId
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

        public async Task<ResponseDto<int>> CreateAsync(CustomerDtoRequest request)
        {
            var response = new ResponseDto<int>();

            try
            {
                response.Result = await _repository.CreateAsync(new Customer
                {
                    Name = request.Name,
                    BirthDate = request.BirthDate,
                    NumberId = request.NumberId
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

        public async Task<ResponseDto<int>> UpdateAsync(int id, CustomerDtoRequest request)
        {
            var response = new ResponseDto<int>();
            try
            {
                await _repository.UpdateAsync(new Customer
                {
                    Id = id,
                    Name = request.Name,
                    BirthDate = request.BirthDate,
                    NumberId = request.NumberId
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
