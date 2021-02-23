using ECommerce.Api.Customers.Domain.QueryModels.CustomerQueryModels.QueryParams;
using ECommerce.Api.Customers.Presentation.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService _productsService;
        public CustomersController(ICustomersService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync(
            [FromQuery] int? limit,
            [FromQuery] int? offset,
            [FromQuery] string search = null)
        {
            var models = await _productsService.GetCustomersAsync(new CustomersQuery(offset, limit, search));

            if (models == null) return NotFound();

            return Ok(models);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCustomersCountAsync()
        {
            return Ok(await _productsService.GetCustomersCountAsync());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryAsync([FromRoute] string id)
        {
            var model = await _productsService.GetCustomerAsync(id);

            if (model == null) return NotFound();

            return Ok(model);
        }
    }
}
