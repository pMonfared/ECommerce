using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using ECommerce.Api.Search.Presentation.Extensions;
using ECommerce.Utilities.ApiConfig;
using ECommerce.Utilities.ErrorHandling.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Search.IoCConfig
{
    public class ServiceConfigs
    {
        public string InternalProductsServiceAddress { get; set; }
        public string InternalOrdersServiceAddress { get; set; }
        public string InternalCustomersServiceAddress { get; set; }
    }

    public static class ConfigureServicesExtensions
    {
        public static void AddAllServices(this IServiceCollection services, ServiceConfigs serviceConfigs)
        {
            //Enable Customize ModelState Filter
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddExceptionServices();
            services.AddExternalClusterServices(serviceConfigs.InternalProductsServiceAddress,
                serviceConfigs.InternalOrdersServiceAddress,
                serviceConfigs.InternalCustomersServiceAddress);

            services.AddInternalServices();


            services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddCustomApiVersion();
        }

    }
}
