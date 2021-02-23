using ECommerce.Api.Orders.Domain.QueryModels.OrderQueryModels.QueryParams;
using ECommerce.Api.Orders.Presentation.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetOrdersAsync(
            [FromRoute] int customerId,
            [FromQuery] int? limit,
            [FromQuery] int? offset,
            [FromQuery] string search = null)
        {
            var models = await _ordersService.GetOrdersAsync(new OrdersQuery(offset, limit, search, customerId));

            if (models == null) return NotFound();

            return Ok(models);
        }

        [HttpGet("customer/{customerId}/count")]
        public async Task<IActionResult> GetOrdersCountAsync(
            [FromRoute] int customerId)
        {
            return Ok(await _ordersService.GetOrdersCountAsync(customerId));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryAsync([FromRoute] string id)
        {
            var model = await _ordersService.GetOrderAsync(id);

            if (model == null) return NotFound();

            return Ok(model);
        }
    }
}
