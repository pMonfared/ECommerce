using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using ECommerce.Api.Search.Presentation.Extensions;
using ECommerce.Utilities.ApiConfig;
using ECommerce.Utilities.ErrorHandling.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Search.IoCConfig
{
    public static class ConfigureServicesExtensions
    {
        public static void AddAllServices(this IServiceCollection services)
        {
            //Enable Customize ModelState Filter
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            
            services.AddExceptionServices();
            services.AddExternalClusterServices();
            services.AddInternalServices();


            services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddCustomApiVersion();
        }

    }
}
