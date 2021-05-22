using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MitoCodeStore.DataAccess;
using MitoCodeStore.Dto;
using System.Linq;
using Microsoft.Extensions.Logging;
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


        public async Task<ICollection<CustomerDto>> GetCollectionAsync(string filter)
        {
            var collection = await _repository.GetCollectionAsync(filter ?? string.Empty);

            return collection.
                Select(p => new CustomerDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    BirthDate = p.BirthDate,
                    NumberId = p.NumberId
                })
                .ToList();

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

        public async Task CreateAsync(CustomerDto request)
        {
            try
            {
                await _repository.CreateAsync(new Customer
                {
                    Name = request.Name,
                    BirthDate = request.BirthDate,
                    NumberId = request.NumberId
                });
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
