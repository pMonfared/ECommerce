using System.Threading.Tasks;
using ECommerce.Api.Orders.Domain.Entities;
using ECommerce.Api.Orders.Domain.QueryModels.OrderQueryModels.QueryParams;
using ECommerce.Utilities.DomainHelpers.Repositories.Contracts;

namespace ECommerce.Api.Orders.Domain.Repositories.Contracts
{
    public interface IOrdersRepository : IBaseRepository<Order, OrdersQuery>
    {
        Task<int> CountAsync(int customerId);
    }
}
