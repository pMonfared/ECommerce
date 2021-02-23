using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.Api.Search.Presentation.ServiceModels.CustomerServiceModels.ServiceResults;

namespace ECommerce.Api.Search.Presentation.Services.Contracts
{
    public interface ICustomersService
    {
        Task<CustomerServiceResult> GetCustomerAsync(
            int id);
    }
}