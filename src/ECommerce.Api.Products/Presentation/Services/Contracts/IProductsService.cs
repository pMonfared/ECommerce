using ECommerce.Api.Products.Domain.QueryModels.ProductModels.QueryParams;
using ECommerce.Api.Products.Presentation.Models.ProductModels.ServiceResults;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Presentation.Services.Contracts
{

    public interface IProductsService
    {
        Task<IEnumerable<ProductServiceResult>> GetProductsAsync(ProductsQuery qp);
        Task<int> GetProductsCountAsync();
        Task<ProductServiceResult> GetProductAsync(string id);
    }
}
