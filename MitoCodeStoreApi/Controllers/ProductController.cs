using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MitoCodeStore.Dto;
using MitoCodeStoreApi.Filters;

namespace MitoCodeStoreApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductDto> _logger;

        public ProductController(ILogger<ProductDto> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            try
            {

                throw new MitcodeException("Ocurrio un error");
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return "Error capturado";
            }
        }
    }
}