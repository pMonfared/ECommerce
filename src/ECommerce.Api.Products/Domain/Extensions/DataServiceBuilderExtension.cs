using ECommerce.Api.Products.Domain.Repositories;
using ECommerce.Api.Products.Domain.Repositories.Contracts;
using ECommerce.Utilities.DomainHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Api.Products.Domain.Extensions
{
    public static class DataServiceBuilderExtension
    {
        private static void AddSchemaDbContext(this IServiceCollection services, string dbConnectionString)
        {
            services.AddDbContext<ProductDbContext>(options =>
            {
                options.UseInMemoryDatabase("Products");
                //options.UseNpgsql(dbConnectionString ?? throw new Exception("Db Core Schema ConnectionString is null"));
            });
        }

        public static void AddSchemaRepository(this IServiceCollection services, string dbConnectionString)
        {
            services.AddSchemaDbContext(dbConnectionString);

            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<ICategoriesRepository, CategoriesRepositoryProvider>();
            services.AddScoped<IProductsRepository, ProductsRepositoryProvider>();
            services.AddScoped<IUnitOfWork, ProductDbContext>();
        }
    }
}
