using Microsoft.AspNetCore.Builder;

namespace ECommerce.Utilities.ErrorHandling.Extensions
{
    public static class ExceptionBuilderExtension
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            return app;
        }
    }
}