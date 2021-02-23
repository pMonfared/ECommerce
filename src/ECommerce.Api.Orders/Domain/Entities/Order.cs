using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Api.Orders.Domain.Entities
{
    public class Order
    {
        public Order()
        {
            Items = new HashSet<OrderItem>();
        }
        [Key]
        public int Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        
        public DateTimeOffset OrderDate { get; set; }

        public int CustomerId { get; set; }
        
        public decimal Total { get; set; }
        
        public virtual  ICollection<OrderItem> Items { get; set; }
    }
}
