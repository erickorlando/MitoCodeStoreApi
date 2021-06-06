using Microsoft.Extensions.Logging;
using MitoCodeStore.DataAccess.Repositories;
using MitoCodeStore.Dto.Request;
using MitoCodeStore.Dto.Response;
using MitoCodeStore.Entities;
using MitoCodeStore.Entities.Complex;
using MitoCodeStore.Services.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MitoCodeStore.Services.Implementations
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _repository;
        private readonly ILogger<ISaleRepository> _logger;

        public SaleService(ISaleRepository repository, ILogger<ISaleRepository> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        private Func<InvoiceInfo, SaleDtoSingleResponse> GetSelector()
        {
            return p => new SaleDtoSingleResponse
            {
                SaleId = p.Id,
                Customer = p.CustomerName,
                InvoiceNumber = p.InvoiceNumber,
                SaleDate = p.SaleDate.ToShortDateString(),
                SaleTotal = p.TotalAmount
            };
        }

        public async Task<SaleDtoResponse> SelectByDate(DateTime dateInit, DateTime dateEnd, int page, int rows)
        {
            var response = new SaleDtoResponse();

            try
            {
                var tuple = await _repository.SelectAsync(dateInit, dateEnd, page, rows);

                response.Collection = tuple.collection
                        .Select(GetSelector())
                        .ToList();

                response.TotalPages = MitoCodeStoreUtils.GetTotalPages(tuple.total, rows);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
            }

            return response;
        }

        public async Task<SaleDtoResponse> SelectByDni(BaseDtoRequest request)
        {
            var response = new SaleDtoResponse();

            try
            {
                var tuple = await _repository.SelectAsync(request.Filter, request.Page, request.Rows);

                response.Collection = tuple.collection
                    .Select(GetSelector())
                    .ToList();

                response.TotalPages = MitoCodeStoreUtils.GetTotalPages(tuple.total, request.Rows);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
            }

            return response;
        }

        public async Task<SaleDtoResponse> SelectByInvoiceNumber(BaseDtoRequest request)
        {
            var response = new SaleDtoResponse();

            try
            {
                var tuple = await _repository.SelectByInvoiceNumberAsync(request.Filter, request.Page, request.Rows);

                response.Collection = tuple.collection
                    .Select(GetSelector())
                    .ToList();

                response.TotalPages = MitoCodeStoreUtils.GetTotalPages(tuple.total, request.Rows);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
            }

            return response;
        }

        public async Task<SaleDetailDtoResponse> SelectDetails(int saleId)
        {
            var response = new SaleDetailDtoResponse();
            var collection = await _repository.GetSaleDetails(saleId);
            response.Details = collection
                .Select(x => new SaleDetailDtoSingleResponse
                {
                    ItemId = x.Id,
                    ProductName = x.ProductName,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    TotalPrice = x.Total
                })
                .ToList();

            return response;
        }

        public async Task<ResponseDto<int>> CreateAsync(SaleDtoRequest request)
        {
            var response = new ResponseDto<int>();

            try
            {
                var entity = new Sale
                {
                    CustomerId = request.CustomerId,
                    PaymentMethodId = request.PaymentMethodId,
                    Date = Convert.ToDateTime(request.Date),
                    TotalAmount = request.Products.Sum(x => x.Quantity * x.UnitPrice)
                };

                var sale = await _repository.CreateAsync(entity);

                foreach (var product in request.Products)
                {
                    await _repository.CreateSaleDetail(new SaleDetail
                    {
                        Sale = entity,
                        ProductId = product.ProductId,
                        Quantity = product.Quantity,
                        UnitPrice = product.UnitPrice,
                        Total = product.Quantity * product.UnitPrice
                    });
                }

                await _repository.CommitTransaction();

                response.Result = sale.Id;
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                response.Success = false;
            }

            return response;
        }

        public async Task<ResponseDto<string>> GeneratePdf()
        {
            var response = new ResponseDto<string>();

            try
            {
                response.Result = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "Assets", "index.html"));
                response.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
            }

            return response;
        }

    }
}