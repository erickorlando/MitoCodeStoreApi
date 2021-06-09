using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MitoCodeStore.Dto.Request;
using MitoCodeStore.Dto.Response;
using MitoCodeStore.Entities;
using MitoCodeStore.Services.Interfaces;
using MitoCodeStoreApi.Filters;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;


namespace MitoCodeStoreApi.Controllers
{
    [Route(Constants.RouteTemplate)]
    [ApiVersion(Constants.V1)]
    [ApiController]
    [TypeFilter(typeof(FiltroRecurso))]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(CustomerDtoResponse))]
        [SwaggerResponse(Constants.Unauthorized, Constants.NoAutorizado)]
        public async Task<IActionResult> List([FromQuery] string filter,
            int page = 1, int rows = 4)
        {
            return Ok(await _service.GetCollectionAsync(new BaseDtoRequest(filter, page, rows)));
        }

        [HttpGet]
        [Route("{id:int}")]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(ResponseDto<CustomerDtoSingleResponse>))]
        [SwaggerResponse(Constants.NotFound, Constants.NoEncontrado, typeof(object))]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.GetCustomerAsync(id);

            return response.Success ? Ok(response) : NotFound();
        }

        [HttpPost]
        [SwaggerResponse(Constants.Created, Constants.Creado)]
        public async Task<IActionResult> Post([FromBody][ModelBinder] CustomerDtoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _service.CreateAsync(request);

            if (response.Success)
                return Created($"Customer/{response.Result}", new
                {
                    id = response.Result
                });

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        [SwaggerResponse(Constants.Accepted, Constants.Aceptado, typeof(int))]
        [SwaggerResponse(Constants.NotFound, Constants.NoEncontrado, typeof(ResponseDto<int>))]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerDtoRequest request)
        {
            var response = await _service.UpdateAsync(id, request);

            if (response.Success)
                return Accepted($"Customer/{response.Result}", new
                {
                    id = response.Result
                });
            return NotFound(id);
        }


        [HttpDelete("{id:int}")]
        [SwaggerResponse(Constants.Accepted, Constants.Aceptado, typeof(int))]
        [SwaggerResponse(Constants.NotFound, Constants.NoEncontrado, typeof(ResponseDto<int>))]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.DeleteAsync(id);
            if (response.Success)
                return Accepted();

            return NotFound(id);
        }
    }
}
