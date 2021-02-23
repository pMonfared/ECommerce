using ECommerce.Api.Products.Presentation.Services.Contracts;
using ECommerce.Api.Products.Presentation.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Api.Products.Presentation.Extensions
{
    public static class ServiceServiceBuilderExtension
    {
        public static void AddIntenalServices(this IServiceCollection services)
        {
            
            services.AddScoped<ICategoriesService, CategoriesServiceProvider>();
            services.AddScoped<IProductsService, ProductsServiceProvider>();
        }
    }
}
