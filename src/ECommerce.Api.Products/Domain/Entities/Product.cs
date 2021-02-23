
using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Api.Products.Domain.Entities
{

    public class Product
    {
        [Key]
        public int Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Inventory { get; set; }
    }
}
