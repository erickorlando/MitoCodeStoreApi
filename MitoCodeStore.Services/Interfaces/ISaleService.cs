using System;
using System.Threading.Tasks;
using MitoCodeStore.Dto.Request;
using MitoCodeStore.Dto.Response;

namespace MitoCodeStore.Services.Interfaces
{
    public interface ISaleService
    {
        Task<SaleDtoResponse> SelectByDate(DateTime dateInit, DateTime dateEnd, int page, int rows);
        Task<SaleDtoResponse> SelectByDni(BaseDtoRequest request);
        Task<SaleDtoResponse> SelectByInvoiceNumber(BaseDtoRequest request);

        Task<SaleDetailDtoResponse> SelectDetails(int saleId);
        Task<ResponseDto<int>> CreateAsync(SaleDtoRequest request);
    }
}