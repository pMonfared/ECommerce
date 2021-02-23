using ECommerce.Api.Products.Domain.Entities;
using ECommerce.Api.Products.Domain.QueryModels.ProductModels.QueryParams;
using ECommerce.Utilities.DomainHelpers.Repositories.Contracts;

namespace ECommerce.Api.Products.Domain.Repositories.Contracts
{
    public interface IProductsRepository : IBaseRepository<Product, ProductsQuery> { }
}
