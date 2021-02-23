using ECommerce.Api.Orders.Presentation.Services.Contracts;
using ECommerce.Api.Orders.Presentation.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Api.Orders.Presentation.Extensions
{
    public static class ServiceServiceBuilderExtension
    {
        public static void AddInternalServices(this IServiceCollection services)
        {
            services.AddScoped<IOrdersService, OrdersServiceProvider>();
        }
    }
}
