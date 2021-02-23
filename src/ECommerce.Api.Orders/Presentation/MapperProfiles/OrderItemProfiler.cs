using ECommerce.Api.Orders.Domain.Entities;
using ECommerce.Api.Orders.Presentation.ServiceModels.OrderItemServiceModels.ServiceResults;

namespace ECommerce.Api.Orders.Presentation.MapperProfiles
{
    public class OrderItemProfiler : AutoMapper.Profile
    {
        public OrderItemProfiler()
        {
            CreateMap<OrderItem, OrderItemServiceResult>();
        }
    }
}