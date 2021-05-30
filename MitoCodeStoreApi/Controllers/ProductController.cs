using Microsoft.AspNetCore.Mvc;
using MitoCodeStore.Dto;
using MitoCodeStore.Dto.Response;
using MitoCodeStore.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace MitoCodeStoreApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        [SwaggerResponse(200, "OK", typeof(ProductDtoResponse))]
        public async Task<IActionResult> Get([FromQuery]string filter, int page = 1, int rows = 4)
        {
            return Ok(await _service.SelectAllAsync(filter, page, rows));
        }

        [HttpGet]
        [Route("{id:int}")]
        [SwaggerResponse(200, "Encontrado", typeof(ResponseDto<ProductSingleDtoResponse>))]
        [SwaggerResponse(404, "Not Found", typeof(object))]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.GetProductAsync(id);

            return response.Success ? Ok(response) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody][ModelBinder] ProductSingleDtoResponse request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _service.CreateAsync(request);

            return Created($"Product/{product.ProductId}", product);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductSingleDtoResponse request)
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