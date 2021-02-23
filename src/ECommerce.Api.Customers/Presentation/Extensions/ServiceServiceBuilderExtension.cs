using ECommerce.Api.Customers.Presentation.Services.Contracts;
using ECommerce.Api.Customers.Presentation.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Api.Customers.Presentation.Extensions
{
    public static class ServiceServiceBuilderExtension
    {
        public static void AddInternalServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomersService, CustomersServiceProvider>();
        }
    }
}
