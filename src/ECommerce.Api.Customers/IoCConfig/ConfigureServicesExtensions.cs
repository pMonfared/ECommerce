using ECommerce.Api.Customers.Domain.Extensions;
using ECommerce.Api.Customers.Presentation.MapperProfiles;
using ECommerce.Api.Customers.Presentation.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using ECommerce.Utilities.ApiConfig;
using ECommerce.Utilities.ErrorHandling.Extensions;

namespace ECommerce.Api.Customers.IoCConfig
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

            services.AddAutoMapper(typeof(CustomerProfiler));

            services.AddSchemaRepository(dbConnectionString);
            services.AddExceptionServices();
            services.AddInternalServices();


            services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddCustomApiVersion();
        }

    }
}
