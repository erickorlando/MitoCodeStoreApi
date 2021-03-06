using Microsoft.Extensions.Logging;
using MitoCodeStore.DataAccess.Repositories;
using MitoCodeStore.Dto.Request;
using MitoCodeStore.Dto.Response;
using MitoCodeStore.Entities;
using MitoCodeStore.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MitoCodeStore.DataAccess;

namespace MitoCodeStore.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerDtoRequest> _logger;
        private readonly UserManager<MitoCodeUserIdentity> _userManager;

        public CustomerService(ICustomerRepository repository, 
            IMapper mapper,
            ILogger<CustomerDtoRequest> logger, 
            UserManager<MitoCodeUserIdentity> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
        }


        public async Task<CustomerDtoResponse> GetCollectionAsync(BaseDtoRequest request)
        {
            var response = new CustomerDtoResponse();

            var tuple = await _repository
                .GetCollectionAsync(request.Filter ?? string.Empty, request.Page, request.Rows);

            response.Collection = tuple.collection
                .Select(p => _mapper.Map<CustomerDtoSingleResponse>(p))
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

                response.Result = _mapper.Map<CustomerDtoSingleResponse>(customer);

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
                response.Result = await _repository.CreateAsync(_mapper.Map<Customer>(request));

                var userName = request.Name.Split(' ').FirstOrDefault() ?? request.Name;
                await _userManager.CreateAsync(new MitoCodeUserIdentity
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
                request.Id = id;
                await _repository.UpdateAsync(_mapper.Map<Customer>(request));

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
