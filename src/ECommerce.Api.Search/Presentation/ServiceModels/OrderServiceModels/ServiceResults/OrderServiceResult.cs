using System;
using System.Collections.Generic;
using ECommerce.Api.Search.Presentation.ServiceModels.OrderItemServiceModels.ServiceResults;

namespace ECommerce.Api.Search.Presentation.ServiceModels.OrderServiceModels.ServiceResults
{
    public class OrderServiceResult
    {
        public OrderServiceResult()
        {
            Items = new HashSet<OrderItemServiceResult>();
        }
        
        public int Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        
        public DateTimeOffset OrderDate { get; set; }

        public int CustomerId { get; set; }
        
        public decimal Total { get; set; }
        public IEnumerable<OrderItemServiceResult> Items { get; set; }
    }
}
