using ECommerce.Utilities.CustomExceptions.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Utilities.ErrorHandling.Extensions
{
    public static class ExceptionServiceExtention
    {
        public static IServiceCollection AddExceptionServices(this IServiceCollection services)
        {
            services.AddCustomException();
            return services;
        }
    }
}