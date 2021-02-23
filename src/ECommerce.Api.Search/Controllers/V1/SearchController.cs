using System.Threading.Tasks;
using ECommerce.Api.Search.Presentation.Services.Contracts;
using ECommerce.Utilities.ApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Search.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class SearchController : CustomControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }


        [HttpGet]
        public async Task<IActionResult> SearchAsync([FromQuery] int customerId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var result = await _searchService.SearchAsync(customerId);
            return Ok(result);
        }
    }
}