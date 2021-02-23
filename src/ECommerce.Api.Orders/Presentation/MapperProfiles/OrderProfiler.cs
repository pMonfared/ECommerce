using ECommerce.Api.Orders.Presentation.ServiceModels.OrderServiceModels.ServiceResults;
using ECommerce.Api.Orders.Domain.Entities;

namespace ECommerce.Api.Orders.Presentation.MapperProfiles
{
    public class OrderProfiler : AutoMapper.Profile
    {
        public OrderProfiler()
        {
            CreateMap<Order, OrderServiceResult>();
        }
    }
}
