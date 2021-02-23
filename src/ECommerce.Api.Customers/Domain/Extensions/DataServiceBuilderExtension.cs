using ECommerce.Api.Customers.Domain.Repositories;
using ECommerce.Api.Customers.Domain.Repositories.Contracts;
using ECommerce.Utilities.DomainHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Api.Customers.Domain.Extensions
{
    public static class DataServiceBuilderExtension
    {
        private static void AddSchemaDbContext(this IServiceCollection services, string dbConnectionString)
        {
            services.AddDbContext<CustomerDbContext>(options =>
            {
                options.UseInMemoryDatabase("Customers");
                //options.UseNpgsql(dbConnectionString ?? throw new Exception("Db Core Schema ConnectionString is null"));
            });
        }

        public static void AddSchemaRepository(this IServiceCollection services, string dbConnectionString)
        {
            services.AddSchemaDbContext(dbConnectionString);

            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<ICustomersRepository, CustomersRepositoryProvider>();
            services.AddScoped<IUnitOfWork, CustomerDbContext>();
        }
    }
}
