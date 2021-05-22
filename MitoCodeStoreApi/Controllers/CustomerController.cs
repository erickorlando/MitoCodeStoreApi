using Microsoft.AspNetCore.Mvc;
using MitoCodeStore.Dto;
using MitoCodeStore.Services;
using MitoCodeStoreApi.Filters;
using System.Collections.Generic;
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
        public async Task<IEnumerable<CustomerDto>> List([FromQuery] string filter)
        {
            return await _service.GetCollectionAsync(filter);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ResponseDto<CustomerDto>> Get(int id)
        {
            return await _service.GetCustomerAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody][ModelBinder] CustomerDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _service.CreateAsync(request);

            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task Update(int id, [FromBody] CustomerDto request)
        {
            await _service.UpdateAsync(id, request);
        }


        [HttpDelete("{id:int}")]
        public async Task Delete(int id)
        {
            await _service.DeleteAsync(id);
        }
    }
}
