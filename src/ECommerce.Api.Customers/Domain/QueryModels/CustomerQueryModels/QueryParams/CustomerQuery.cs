using ECommerce.Utilities.DomainHelpers.Queries;

namespace ECommerce.Api.Customers.Domain.QueryModels.CustomerQueryModels.QueryParams
{
    public class CustomersQuery : QueryParam
    {
        public CustomersQuery(int? offset, int? limit, string search) : base(offset, limit, search)
        {
        }
    }
}
