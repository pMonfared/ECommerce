using ECommerce.Api.Orders.Domain.QueryModels.OrderQueryModels.QueryParams;
using ECommerce.Api.Orders.Presentation.ServiceModels.OrderServiceModels.ServiceResults;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Presentation.Services.Contracts
{
    public interface IOrdersService
    {
        Task<IEnumerable<OrderServiceResult>> GetOrdersAsync(OrdersQuery qp);
        Task<int> GetOrdersCountAsync(int customerId);
        Task<OrderServiceResult> GetOrderAsync(string id);
    }
}
