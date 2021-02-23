using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Utilities.ApiResponse.Extensions
{
    public static class ApiResponseBuilderExtensions
    {
        public static IServiceCollection AddApiResponseService(this IServiceCollection services)
        {
            services.AddTransient<IResponseGenerator, ResponseGenerator>();
            return services;
        }
    }
}