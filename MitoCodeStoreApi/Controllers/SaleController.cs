using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MitoCodeStore.Dto.Request;
using MitoCodeStore.Dto.Response;
using MitoCodeStore.Entities;
using MitoCodeStore.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace MitoCodeStoreApi.Controllers
{
    [ApiController]
    [ApiVersion(Constants.V1)]
    [Route(Constants.RouteTemplate)]
    [Authorize]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _service;
        private readonly IProductService _productService;

        public SaleController(ISaleService service, IProductService productService)
        {
            _service = service;
            _productService = productService;
        }

        [HttpGet]
        [Route("ProductsGrid")]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(ProductsGridDtoResponse))]
        public async Task<IActionResult> ProductsGrid()
        {
            return Ok(await _productService.GetCollectionAsync(new BaseDtoRequest(string.Empty, 1, 100)));
        }


        [HttpGet]
        [Route("List")]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(SaleDtoResponse))]
        public async Task<IActionResult> List([FromQuery] int page = 1, int rows = 4, string initialdate = "", string finaldate = "", string dni = "", string number = "")
        {
            SaleDtoResponse response;

            if (!string.IsNullOrEmpty(initialdate) && !string.IsNullOrEmpty(finaldate))
                response = await _service.SelectByDate(Convert.ToDateTime(initialdate), Convert.ToDateTime(finaldate), page, rows);
            else if (!string.IsNullOrEmpty(dni))
                response = await _service.SelectByDni(new BaseDtoRequest(dni, page, rows));
            else
                response = await _service.SelectByInvoiceNumber(new BaseDtoRequest(number, page, rows));

            return Ok(response);
        }

        [HttpGet]
        [Route("GetDetail/{saleId:int}")]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(SaleDetailDtoResponse))]
        public async Task<IActionResult> GetDetail(int saleId)
        {
            return Ok(await _service.SelectDetails(saleId));
        }

        [HttpPost]
        [Route("Create")]
        [SwaggerResponse(Constants.Created, Constants.Creado, typeof(ResponseDto<int>))]
        [SwaggerResponse(Constants.BadRequest, Constants.NoValido)]
        public async Task<IActionResult> Create([FromBody] SaleDtoRequest request)
        {
            var response = await _service.CreateAsync(request);

            if (response.Success)
                return Created($"Sale/GetDetail/{response.Result}", new { id = response.Result });

            return BadRequest();
        }

    }
}