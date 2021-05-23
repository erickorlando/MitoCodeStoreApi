using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MitoCodeStore.DataAccess;
using MitoCodeStore.Dto;
using System.Linq;
using System.Xml.Schema;
using Microsoft.Extensions.Logging;
using MitoCodeStore.Dto.Response;
using MitoCodeStore.Entities;

namespace MitoCodeStore.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CustomerDto> _logger;

        public CustomerService(ICustomerRepository repository, ILogger<CustomerDto> logger)
        {
            _repository = repository;
            _logger = logger;
        }


        public async Task<CustomerDtoResponse> GetCollectionAsync(string filter, int page, int rows)
        {
            var response = new CustomerDtoResponse();

            var repositoryResult = await _repository
                .GetCollectionAsync(filter ?? string.Empty, page, rows);

            response.Customers = repositoryResult.collection
                .Select(p => new CustomerDtoSingleResponse()
                {
                    CustomerId = p.Id,
                    CustomerName = p.Name,
                    CustomerBirth = p.BirthDate.ToShortDateString(),
                    CustomerNumberId = p.NumberId
                })
                .ToList();

            var totalPages = repositoryResult.total / rows;
            if (repositoryResult.total % rows > 0)
                totalPages++;

            response.TotalPages = totalPages;

            return response;

        }

        public async Task<ResponseDto<CustomerDto>> GetCustomerAsync(int id)
        {
            var response = new ResponseDto<CustomerDto>();
            var customer = await _repository.GetItemAsync(id);

            if (customer == null)
            {
                response.Success = false;
                return response;
            }

            response.Result = new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                BirthDate = customer.BirthDate,
                NumberId = customer.NumberId
            };

            response.Success = true;

            return response;
        }

        public async Task<CustomerDto> CreateAsync(CustomerDto request)
        {
            try
            {
                var result = await _repository.CreateAsync(new Customer
                {
                    Name = request.Name,
                    BirthDate = request.BirthDate,
                    NumberId = request.NumberId
                });

                return new CustomerDto
                {
                    Id = result.Id,
                    Name = result.Name,
                    BirthDate = result.BirthDate,
                    NumberId = result.NumberId
                };
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                throw;
            }
        }

        public async Task UpdateAsync(int id, CustomerDto request)
        {
            await _repository.UpdateAsync(id, new Customer
            {
                Name = request.Name,
                BirthDate = request.BirthDate,
                NumberId = request.NumberId
            });
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
