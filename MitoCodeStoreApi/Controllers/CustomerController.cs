using Microsoft.AspNetCore.Mvc;
using MitoCodeStore.Dto;
using MitoCodeStore.Dto.Response;
using MitoCodeStore.Services;
using MitoCodeStoreApi.Filters;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;


namespace MitoCodeStoreApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [TypeFilter(typeof(FiltroRecurso))]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(CustomerDtoResponse))]
        public async Task<IActionResult> List([FromQuery] string filter,
            int page = 1, int rows = 4)
        {
            return Ok(await _service.GetCollectionAsync(filter, page, rows));
        }

        [HttpGet]
        [Route("{id:int}")]
        [SwaggerResponse(200, "Encontrado", typeof(ResponseDto<CustomerDto>))]
        [SwaggerResponse(404, "Not Found", typeof(object))]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.GetCustomerAsync(id);

            return response.Success ? Ok(response) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody][ModelBinder] CustomerDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _service.CreateAsync(request);

            return Created($"Customer/{customer.Id}", customer);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerDto request)
        {
            await _service.UpdateAsync(id, request);
            return Accepted();
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return Accepted();
        }
    }
}
