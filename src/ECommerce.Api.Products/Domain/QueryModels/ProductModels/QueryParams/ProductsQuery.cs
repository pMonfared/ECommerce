using System;
using ECommerce.Utilities.DomainHelpers.Queries;

namespace ECommerce.Api.Products.Domain.QueryModels.ProductModels.QueryParams
{
    public class ProductsQuery : QueryParam
    {
        public int? CategoryId { get; set; }
        public int? Price { get; set; }
        public int? Inventory { get; set; }

        public DateTimeOffset? CreatedAtFrom { get; set; }
        public DateTimeOffset? CreatedAtTo { get; set; }

        public ProductsQuery() : base(null, null, null)
        {
            
        }
        
        public ProductsQuery(int? offset, int? limit,  string search,
            int? categoryId, int? price,int? inventory,
            DateTimeOffset? createdAtFrom, DateTimeOffset? createdAtTo) : base(offset, limit,  search)
        {
            CategoryId = categoryId;
            Price = price;
            Inventory = inventory;
            CreatedAtFrom = createdAtFrom;
            CreatedAtTo = createdAtTo;
        }
    }
}
