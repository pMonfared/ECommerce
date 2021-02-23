using ECommerce.Api.Products.Presentation.Models.CategoryModels.ServiceResults;
using System;

namespace ECommerce.Api.Products.Presentation.Models.ProductModels.ServiceResults
{
    public class ProductServiceResult
    {
        public ProductServiceResult()
        {
            Category = new CategoryServiceResult();
        }
        public int Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Inventory { get; set; }

        public CategoryServiceResult Category { get; set; }
    }
}
