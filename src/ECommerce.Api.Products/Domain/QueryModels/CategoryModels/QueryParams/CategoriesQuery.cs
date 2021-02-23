using ECommerce.Utilities.DomainHelpers.Queries;

namespace ECommerce.Api.Products.Domain.QueryModels.CategoryModels.QueryParams
{
    public class CategoriesQuery : QueryParam
    {
        public CategoriesQuery(int? offset, int? limit, string search) : base(offset, limit, search)
        {
        }
    }
}
