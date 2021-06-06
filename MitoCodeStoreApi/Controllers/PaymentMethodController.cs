using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MitoCodeStore.Dto.Request;
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
    [Authorize]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethodService _service;

        public PaymentMethodController(IPaymentMethodService service)
        {
            _service = service;
        }

        [HttpGet]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(PaymentMethodDtoResponse))]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetCollectionAsync());
        }

        [HttpGet]
        [Route("{id:int}")]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(PaymentMethodDtoSingleResponse))]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.GetItemAsync(id);

            if (response.Success)
                return Ok(response.Result);

            return NotFound();
        }

        [HttpPost]
        [SwaggerResponse(Constants.Created, Constants.Creado, typeof(ResponseDto<int>))]
        public async Task<IActionResult> Create([FromBody] PaymentMethodDtoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _service.CreateAsync(request);
            if (response.Success)
                return Created($"PaymentMethod/{response.Result}", new {id = response.Result});

            return BadRequest();
        }
    }
}