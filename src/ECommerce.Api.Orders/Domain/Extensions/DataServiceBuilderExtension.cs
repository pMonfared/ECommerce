using ECommerce.Api.Orders.Domain.Repositories;
using ECommerce.Api.Orders.Domain.Repositories.Contracts;
using ECommerce.Utilities.DomainHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Api.Orders.Domain.Extensions
{
    public static class DataServiceBuilderExtension
    {
        private static void AddSchemaDbContext(this IServiceCollection services, string dbConnectionString)
        {
            services.AddDbContext<OrderDbContext>(options =>
            {
                options.UseInMemoryDatabase("Orders");
                //options.UseNpgsql(dbConnectionString ?? throw new Exception("Db Core Schema ConnectionString is null"));
            });
        }

        public static void AddSchemaRepository(this IServiceCollection services, string dbConnectionString)
        {
            services.AddSchemaDbContext(dbConnectionString);

            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IOrdersRepository, OrdersRepositoryProvider>();
            services.AddScoped<IUnitOfWork, OrderDbContext>();
        }
    }
}
