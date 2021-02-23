using ECommerce.Api.Customers.Domain.Entities;
using ECommerce.Api.Customers.Presentation.ServiceModels.CustomerServiceModels.ServiceResults;

namespace ECommerce.Api.Customers.Presentation.MapperProfiles
{
    public class CustomerProfiler : AutoMapper.Profile
    {
        public CustomerProfiler()
        {
            CreateMap<Customer, CustomerServiceResult>();
        }
    }
}
