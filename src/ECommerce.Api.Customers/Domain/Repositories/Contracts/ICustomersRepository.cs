using ECommerce.Api.Customers.Domain.Entities;
using ECommerce.Api.Customers.Domain.QueryModels.CustomerQueryModels.QueryParams;
using ECommerce.Utilities.DomainHelpers.Repositories.Contracts;

namespace ECommerce.Api.Customers.Domain.Repositories.Contracts
{
    public interface ICustomersRepository : IBaseRepository<Customer, CustomersQuery> { }
}
