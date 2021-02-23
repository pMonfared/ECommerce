using ECommerce.Utilities.ApiResponse.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Utilities.CustomExceptions.Extensions
{
    public static class CustomExceptionServiceExtention
    {
        public static void AddCustomException(this IServiceCollection services)
        {
            services.AddApiResponseService();
        }
    }
}