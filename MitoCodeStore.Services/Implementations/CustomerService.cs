using Microsoft.Extensions.Logging;
using MitoCodeStore.DataAccess.Repositories;
using MitoCodeStore.Dto.Request;
using MitoCodeStore.Dto.Response;
using MitoCodeStore.Entities;
using MitoCodeStore.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MitoCodeStore.DataAccess;

namespace MitoCodeStore.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CustomerDtoRequest> _logger;
        private readonly UserManager<MitoCodeUserIdentity> _userManager;

        public CustomerService(ICustomerRepository repository, ILogger<CustomerDtoRequest> logger, UserManager<MitoCodeUserIdentity> userManager)
        {
            _repository = repository;
            _logger = logger;
            _userManager = userManager;
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
                    CustomerBirth = p.BirthDate.ToString(Constants.DateFormat),
                    CustomerNumberId = p.NumberId,
                    CustomerEmail = p.Email
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
                    CustomerNumberId = customer.NumberId,
                    CustomerEmail = customer.Email
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
                    NumberId = request.NumberId,
                    Email = request.Email
                });

                var userName = request.Name.Split(' ').FirstOrDefault() ?? request.Name;
                var result = await _userManager.CreateAsync(new MitoCodeUserIdentity
                {
                    FirstName = request.Name,
                    BirthDate = Convert.ToDateTime(request.BirthDate),
                    Email = request.Email,
                    UserName = userName
                }, "12345678");

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
                    NumberId = request.NumberId,
                    Email = request.Email
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
