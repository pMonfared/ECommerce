using ECommerce.Api.Orders.Domain.Extensions;
using ECommerce.Api.Orders.Presentation.MapperProfiles;
using ECommerce.Api.Orders.Presentation.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using ECommerce.Utilities.ApiConfig;
using ECommerce.Utilities.ErrorHandling.Extensions;

namespace ECommerce.Api.Orders.IoCConfig
{
    public static class ConfigureServicesExtensions
    {
        public static void AddAllServices(this IServiceCollection services, string dbConnectionString)
        {
            //Enable Customize ModelState Filter
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            
            services.AddMemoryCache();

            services.AddAutoMapper(typeof(OrderProfiler));

            services.AddSchemaRepository(dbConnectionString);
            services.AddExceptionServices();
            services.AddInternalServices();


            services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddCustomApiVersion();
        }

    }
}
