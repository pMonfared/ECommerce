using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.Api.Search.Presentation.ServiceModels.OrderServiceModels.ServiceResults;

namespace ECommerce.Api.Search.Presentation.Services.Contracts
{
    public interface IOrdersService
    {
        Task<IEnumerable<OrderServiceResult>> GetOrdersAsync(
            int customerId);
    }
}