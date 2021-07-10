using Microsoft.AspNetCore.Mvc;
using MitoCodeStore.Dto.Response;
using MitoCodeStore.Entities;
using MitoCodeStore.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace MitoCodeStoreApi.Controllers
{
    [ApiController]
    [ApiVersion(Constants.V1)]
    [Route(Constants.RouteTemplate)]
    public class ReportController : ControllerBase
    {
        private readonly ISaleService _service;

        public ReportController(ISaleService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("Graph")]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(ReportDtoResponse))]
        public async Task<IActionResult> Graph(int month)
        {
            return Ok(await _service.ReportSalesByMonthAsync(month));
        }
    }
}