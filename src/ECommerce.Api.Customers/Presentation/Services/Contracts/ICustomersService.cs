using ECommerce.Api.Customers.Domain.QueryModels.CustomerQueryModels.QueryParams;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.Api.Customers.Presentation.ServiceModels.CustomerServiceModels.ServiceResults;

namespace ECommerce.Api.Customers.Presentation.Services.Contracts
{
    public interface ICustomersService
    {
        Task<IEnumerable<CustomerServiceResult>> GetCustomersAsync(CustomersQuery qp);
        Task<int> GetCustomersCountAsync();
        Task<CustomerServiceResult> GetCustomerAsync(string id);
    }
}
