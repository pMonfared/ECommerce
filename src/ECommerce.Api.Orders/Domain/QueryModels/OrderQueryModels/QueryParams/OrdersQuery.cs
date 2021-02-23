using ECommerce.Utilities.DomainHelpers.Queries;

namespace ECommerce.Api.Orders.Domain.QueryModels.OrderQueryModels.QueryParams
{
    public class OrdersQuery : QueryParam
    {
        public int CustomerId { get; set; } 
        public OrdersQuery(int? offset, int? limit, string search,int customerId) : base(offset, limit, search)
        {
            CustomerId = customerId;
        }
    }
}
