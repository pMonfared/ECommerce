using ECommerce.Api.Products.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Domain
{
    public interface IDbInitializer
    {
        /// <summary>
        /// Applies any pending migrations for the context to the database.
        /// Will create the database if it does not already exist.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Adds some default values to the Db
        /// </summary>
        void SeedData();
    }

    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
        }

        public void Initialize()
        {
            using var serviceScope = _scopeFactory.CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<ProductDbContext>();
            context.Database.EnsureCreated();
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ProductDbContext>())
                {

                    //add admin user
                    if (!context.Categories.Any())
                    {

                        context.Categories.AddRange(
                            new Category()
                            {
                                Id = 1,
                                Name = "Shoes"
                            },
                    new Category()
                    {
                        Id = 2,
                        Name = "Computers"
                    },
                    new Category()
                    {
                        Id = 3,
                        Name = "Super Market"
                    },
                    new Category()
                    {
                        Id = 4,
                        Name = "Sports equipment"
                    }
                            );
                    }

                    if (!context.Products.Any())
                    {
                        context.Products.AddRange(
                            new Product() { Id = 1, Name = "Sandal", Price = 3, Inventory = 10, CategoryId = 1 , CreatedAt = DateTimeOffset.UtcNow.AddDays(-4) },
                 new Product() { Id = 2, Name = "Sport", Price = 7, Inventory = 120, CategoryId = 1, CreatedAt = DateTimeOffset.UtcNow.AddDays(-6) },
                 new Product() { Id = 3, Name = "Beach", Price = 10, Inventory = 60, CategoryId = 1, CreatedAt = DateTimeOffset.UtcNow.AddDays(-7) },
                 new Product() { Id = 4, Name = "Ballet", Price = 25, Inventory = 90, CategoryId = 1, CreatedAt = DateTimeOffset.UtcNow.AddDays(-10) },
                  new Product() { Id = 5, Name = "Keyboard", Price = 100, Inventory = 30, CategoryId = 2, CreatedAt = DateTimeOffset.UtcNow },
                  new Product() { Id = 6, Name = "Mouse", Price = 7, Inventory = 100, CategoryId = 2, CreatedAt = DateTimeOffset.UtcNow.AddDays(-1) },
                  new Product() { Id = 7, Name = "Monitor", Price = 150, Inventory = 40, CategoryId = 2, CreatedAt = DateTimeOffset.UtcNow.AddDays(-10) },
                  new Product() { Id = 8, Name = "HeadPhone", Price = 20, Inventory = 70, CategoryId = 2, CreatedAt = DateTimeOffset.UtcNow.AddDays(-20) },
                   new Product() { Id = 9, Name = "Cookie", Price = 1, Inventory = 30, CategoryId = 3, CreatedAt = DateTimeOffset.UtcNow.AddDays(-30) },
                   new Product() { Id = 10, Name = "Tea", Price = 3, Inventory = 100, CategoryId = 3, CreatedAt = DateTimeOffset.UtcNow.AddDays(-15) },
                   new Product() { Id = 11, Name = "Coffee", Price = 2, Inventory = 40, CategoryId = 3, CreatedAt = DateTimeOffset.UtcNow.AddDays(-1) },
                   new Product() { Id = 12, Name = "Bread", Price = 1, Inventory = 70, CategoryId = 3, CreatedAt = DateTimeOffset.UtcNow.AddDays(-10) },
                    new Product() { Id = 13, Name = "Football helmet", Price = 30, Inventory = 245, CategoryId = 4, CreatedAt = DateTimeOffset.UtcNow.AddDays(-40) },
                    new Product() { Id = 14, Name = "Elbow pads", Price = 70, Inventory = 420, CategoryId = 4, CreatedAt = DateTimeOffset.UtcNow.AddDays(-10) },
                    new Product() { Id = 15, Name = "Bicycle helmet", Price = 87, Inventory = 300, CategoryId = 4, CreatedAt = DateTimeOffset.UtcNow.AddDays(-10) },
                    new Product() { Id = 16, Name = "Sports gloves", Price = 20, Inventory = 10, CategoryId = 4, CreatedAt = DateTimeOffset.UtcNow.AddDays(-10) }
                            );
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
