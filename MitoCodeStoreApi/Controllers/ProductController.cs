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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }


        [HttpGet]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(ProductDtoResponse))]
        public async Task<IActionResult> Get([FromQuery] string filter, int page = 1, int rows = 4)
        {
            return Ok(await _service.GetCollectionAsync(new BaseDtoRequest(filter, page, rows)));
        }

        [HttpGet]
        [Route("{id:int}")]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(ResponseDto<ProductDtoSingleResponse>))]
        [SwaggerResponse(Constants.NotFound, Constants.NoEncontrado, typeof(ResponseDto<ProductDtoSingleResponse>))]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.GetProductAsync(id);

            return response.Success ? Ok(response) : NotFound();
        }

        [HttpPost]
        [SwaggerResponse(Constants.Created, Constants.Creado)]
        public async Task<IActionResult> Post([FromBody][ModelBinder] ProductDtoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _service.CreateAsync(request);

            if (response.Success)
                return Created($"Product/{response.Result}", response.Result);

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        [SwaggerResponse(202, "Aceptado", typeof(int))]
        [SwaggerResponse(404, "No se encontro registro")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDtoRequest request)
        {
            var response = await _service.UpdateAsync(id, request);
            if (response.Success)
                return AcceptedAtAction("Get", response.Result, request);

            return NotFound(id);
        }


        [HttpDelete("{id:int}")]
        [SwaggerResponse(202, "Aceptado", typeof(int))]
        [SwaggerResponse(404, "No se encontro registro")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.DeleteAsync(id);

            if (response.Success)
                return Accepted();

            return NotFound(id);
        }
    }
}