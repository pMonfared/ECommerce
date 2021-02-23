using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Api.Products.Domain.Entities
{
    public class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
