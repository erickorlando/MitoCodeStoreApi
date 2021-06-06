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
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _repository;
        private readonly ILogger<IPaymentMethodRepository> _logger;

        public PaymentMethodService(IPaymentMethodRepository repository, ILogger<IPaymentMethodRepository> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<PaymentMethodDtoResponse> GetCollectionAsync()
        {
            var response = new PaymentMethodDtoResponse();

            var collection = await _repository.GetCollectionAsync();

            response.Collection = collection.Select(p => new PaymentMethodDtoSingleResponse
            {
                Id = p.Id,
                PaymentMethodName = p.Description
            })
                .ToList();

            return response;
        }

        public async Task<ResponseDto<PaymentMethodDtoSingleResponse>> GetItemAsync(int id)
        {
            var response = new ResponseDto<PaymentMethodDtoSingleResponse>();

            try
            {
                var payment = await _repository.GetItemAsync(id);

                response.Result = new PaymentMethodDtoSingleResponse
                {
                    Id = payment.Id,
                    PaymentMethodName = payment.Description
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

        public async Task<ResponseDto<int>> CreateAsync(PaymentMethodDtoRequest request)
        {
            var response = new ResponseDto<int>();
            try
            {
                response.Result = await _repository.CreateAsync(new PaymentMethod
                {
                    Description = request.Name
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

        public async Task<ResponseDto<int>> UpdateAsync(int id, PaymentMethodDtoRequest request)
        {
            var response = new ResponseDto<int>();
            try
            {
                await _repository.UpdateAsync(new PaymentMethod
                {
                    Id = id,
                    Description = request.Name
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