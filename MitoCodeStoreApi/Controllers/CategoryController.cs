using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MitoCodeStore.Dto.Request;
using MitoCodeStore.Dto.Response;
using MitoCodeStore.Entities;
using MitoCodeStore.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace MitoCodeStoreApi.Controllers
{
    [ApiController]
    [ApiVersion(Constants.V1)]
    [Route(Constants.RouteTemplate)]
    [Authorize]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        
        [HttpGet]
        [SwaggerResponse(200, "OK", typeof(ProductDtoResponse))]
        public async Task<IActionResult> Get([FromQuery] string filter, int page = 1, int rows = 4)
        {
            return Ok(await _service.GetCollectionAsync(new BaseDtoRequest(filter, page, rows)));
        }

        [HttpGet]
        [Route("{id:int}")]
        [SwaggerResponse(200, "Encontrado", typeof(ResponseDto<CategoryDtoRequest>))]
        [SwaggerResponse(404, "Not Found", typeof(ResponseDto<CategoryDtoRequest>))]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.GetAsync(id);

            return response.Success ? Ok(response) : NotFound();
        }

        [HttpPost]
        [SwaggerResponse(201, "Creado")]
        public async Task<IActionResult> Post([FromBody][ModelBinder] CategoryDtoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _service.CreateAsync(request);

            if (response.Success)
                return Created($"Category/{response.Result}", response.Result);

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        [SwaggerResponse(202, "Aceptado", typeof(int))]
        [SwaggerResponse(404, "No se encontro registro")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDtoRequest request)
        {
            var response = await _service.UpdateAsync(id, request);
            if (response.Success)
                return Accepted($"Category/{response.Result}");

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