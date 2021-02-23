using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Api.Products.Domain;
using ECommerce.Api.Products.Domain.Entities;
using ECommerce.Api.Products.Domain.QueryModels.ProductModels.QueryParams;
using ECommerce.Api.Products.Domain.Repositories;
using ECommerce.Api.Products.Domain.Repositories.Contracts;
using ECommerce.Api.Products.Presentation.MapperProfiles;
using ECommerce.Api.Products.Presentation.Services;
using ECommerce.Utilities.DomainHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Xunit;
using ILogger = Serilog.ILogger;

namespace ECommerce.Api.Products.Tests
{
    public class ProductsServiceTest
    {
        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {
            var productsProvider = ProductProviderSetup(nameof(GetProductsReturnsAllProducts));
            var products = await productsProvider.GetProductsAsync(new ProductsQuery());
            
            Assert.True(products != null);
            Assert.True(products.Any());
        }
        
        [Fact]
        public async Task GetProductReturnsProductUsingValidId()
        {
            var productsProvider = ProductProviderSetup(nameof(GetProductReturnsProductUsingValidId));
            var product = await productsProvider.GetProductAsync("1");
            
            Assert.True(product != null);
            Assert.True(product.Id.Equals(1));
            Assert.NotNull(product.Category);
        }
        
        [Fact]
        public async Task GetProductReturnsProductUsingInValidId()
        {
            var productsProvider = ProductProviderSetup(nameof(GetProductReturnsProductUsingInValidId));
            var product = await productsProvider.GetProductAsync("-1");
            
            Assert.True(product == null);
        }

        private ProductsServiceProvider ProductProviderSetup(string dbName)
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(dbName).Options;
            
            var dbContext = new ProductDbContext(options);
            CreateProducts(dbContext);
            
            IUnitOfWork unitOfWork = dbContext;
            IProductsRepository productsRepository = new ProductsRepositoryProvider(unitOfWork,null);
            var productProfile = new ProductProfiler();
            var categoryProfile = new CategoryProfiler();
            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile(productProfile);
                cfg.AddProfile(categoryProfile);
            });
            var mapper = new Mapper(configuration);
            IMemoryCache cache = new MemoryCache(new MemoryCacheOptions
            {
                
            });
            return new ProductsServiceProvider(unitOfWork, productsRepository, null, cache, mapper);
        }

        private void CreateProducts(ProductDbContext dbContext)
        {
            for (int i = 1; i <= 10; i++)
            {
                dbContext.Categories.Add(new Category
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString()
                });
                dbContext.Products.Add(new Product
                {
                    Id = i,
                    CategoryId = i,
                    CreatedAt = DateTimeOffset.UtcNow.AddDays(-1),
                    Inventory = i + 10,
                    Price = (decimal) (i *3.14),
                    Name = Guid.NewGuid().ToString()
                });

                dbContext.SaveChanges();
            }
        }
    }
}
