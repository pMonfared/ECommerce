using ECommerce.Api.Products.Domain.QueryModels.CategoryModels.QueryParams;
using ECommerce.Api.Products.Presentation.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _productsService;
        public CategoriesController(ICategoriesService productsService)
        {
            _productsService = productsService;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetCategoriesAsync(
            [FromQuery] int? limit = 10,
            [FromQuery] int? offset = 0,
            [FromQuery] string search = null)
        {
            var models = await _productsService.GetCategoriesAsync(new CategoriesQuery(offset, limit, search));

            if (models == null) return NotFound();

            return Ok(models);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCategoriesCountAsync()
        {
            return Ok(await _productsService.GetCategoriesCountAsync());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryAsync([FromRoute] string id)
        {
            var model = await _productsService.GetCategoryAsync(id);

            if (model == null) return NotFound();

            return Ok(model);
        }
    }
}
