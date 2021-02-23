using ECommerce.Api.Products.Domain.QueryModels.ProductModels.QueryParams;
using ECommerce.Api.Products.Presentation.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync(
            [FromQuery] int? categoryId,
            [FromQuery] int? limit,
            [FromQuery] int? offset,
            [FromQuery] string search = null,
            [FromQuery] int? price = null, [FromQuery] int? inventory = null,
            [FromQuery] DateTimeOffset? createdAtFrom = null, [FromQuery] DateTimeOffset? createdAtTo = null)
        {
            var models = await _productsService.GetProductsAsync(new ProductsQuery(offset, limit, search, categoryId, price, inventory,createdAtFrom,createdAtTo));

            if (models == null) return NotFound();

            return Ok(models);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetProductsCountAsync()
        {
            return Ok(await _productsService.GetProductsCountAsync());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync([FromRoute] string id)
        {
            var model = await _productsService.GetProductAsync(id);

            if (model == null) return NotFound();

            return Ok(model);
        }
    }
}
