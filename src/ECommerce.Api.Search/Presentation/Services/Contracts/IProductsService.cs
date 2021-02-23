using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.Api.Search.Presentation.ServiceModels.ProductModels.ServiceResults;

namespace ECommerce.Api.Search.Presentation.Services.Contracts
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductServiceResult>> GetProductsAsync();
        Task<ProductServiceResult> GetProductAsync(int productId);
    }
}