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
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(RegisterUserDtoResponse))]
        [SwaggerResponse(Constants.BadRequest, Constants.NoValido, typeof(RegisterUserDtoResponse))]
        public async Task<IActionResult> Register([FromBody][ModelBinder] RegisterUserDtoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _service.RegisterAsync(request);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        [SwaggerResponse(Constants.Ok, Constants.Listo, typeof(LoginDtoResponse))]
        [SwaggerResponse(Constants.BadRequest, Constants.NoValido, typeof(LoginDtoResponse))]
        public async Task<IActionResult> Login([FromBody][ModelBinder] LoginDtoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _service.LoginAsync(request);
            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        
    }
}