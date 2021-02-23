using System;
using ECommerce.Api.Search.Presentation.Services;
using ECommerce.Api.Search.Presentation.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace ECommerce.Api.Search.Presentation.Extensions
{
    public static class ServiceServiceBuilderExtension
    {
        public static void AddInternalServices(this IServiceCollection services)
        {
            services.AddScoped<ISearchService, SearchServiceProvider>();
        }

        public static void AddExternalClusterServices(this IServiceCollection services)
        {
            services.AddScoped<IOrdersService, OrdersExternalServiceProvider>();
            services.AddHttpClient(nameof(OrdersExternalServiceProvider), config =>
            {
                config.BaseAddress = new Uri("http://localhost:3005");
            }).AddTransientHttpErrorPolicy( p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(300)));
            
            services.AddScoped<IProductsService, ProductsExternalServiceProvider>();
            services.AddHttpClient(nameof(ProductsExternalServiceProvider), config =>
            {
                config.BaseAddress = new Uri("http://localhost:3006");
            }).AddTransientHttpErrorPolicy( p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(300)));
            
            
            services.AddScoped<ICustomersService, CustomersExternalServiceProvider>();
            services.AddHttpClient(nameof(CustomersExternalServiceProvider), config =>
            {
                config.BaseAddress = new Uri("http://localhost:3004");
            }).AddTransientHttpErrorPolicy( p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(300)));
        }
    }
}
