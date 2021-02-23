using ECommerce.Api.Products.Domain.Extensions;
using ECommerce.Api.Products.Presentation.MapperProfiles;
using ECommerce.Api.Products.Presentation.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using ECommerce.Utilities.ApiConfig;
using ECommerce.Utilities.ErrorHandling.Extensions;

namespace ECommerce.Api.Products.IoCConfig
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

            services.AddAutoMapper(typeof(ProductProfiler));

            services.AddSchemaRepository(dbConnectionString);
            services.AddExceptionServices();
            services.AddIntenalServices();


            services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddCustomApiVersion();
        }

    }
}
