using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Utilities.ApiConfig
{
    public static class ConfigureApiExtensions
    {
        public static void AddCustomApiVersion(this IServiceCollection services)
        {
            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                o.ReportApiVersions = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                // Versioning using media type
                // clients request the specific version using the X-version header
                o.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("X-version"), new QueryStringApiVersionReader("api-version"));

            });

            services.AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = "'V'VVV";
                o.SubstituteApiVersionInUrl = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
            });
        }
    }
}