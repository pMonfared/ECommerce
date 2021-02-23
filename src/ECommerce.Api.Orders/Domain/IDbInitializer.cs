using ECommerce.Api.Orders.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Api.Orders.Domain
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
            using var context = serviceScope.ServiceProvider.GetService<OrderDbContext>();
            context.Database.EnsureCreated();
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<OrderDbContext>())
                {

                    //add admin user
                    if (!context.Orders.Any())
                    {

                        context.Orders.AddRange(
                            new Order()
                            {
                                Id = 1,
                                CreatedAt = DateTimeOffset.UtcNow.AddDays(-4),
                                CustomerId = 1,
                                OrderDate = DateTimeOffset.UtcNow.AddDays(-4).AddHours(5),
                                Items = new List<OrderItem>()
                                {
                                    new OrderItem() {OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10},
                                    new OrderItem() {OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10},
                                    new OrderItem() {OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10},
                                    new OrderItem() {OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10},
                                    new OrderItem() {OrderId = 1, ProductId = 3, Quantity = 1, UnitPrice = 100}
                                },
                                Total = 100
                            },
                            new Order()
                            {
                                Id = 2,
                                CreatedAt = DateTimeOffset.UtcNow.AddDays(-10),
                                CustomerId = 2,
                                OrderDate = DateTimeOffset.UtcNow.AddDays(-10).AddHours(10),
                                Items = new List<OrderItem>()
                                {
                                    new OrderItem() {OrderId = 2, ProductId = 1, Quantity = 10, UnitPrice = 10},
                                    new OrderItem() {OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10},
                                    new OrderItem() {OrderId = 2, ProductId = 3, Quantity = 10, UnitPrice = 10},
                                    new OrderItem() {OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10},
                                    new OrderItem() {OrderId = 2, ProductId = 3, Quantity = 1, UnitPrice = 100}
                                },
                                Total = 300
                            },
                            new Order()
                            {
                                Id = 3,
                                CreatedAt = DateTimeOffset.UtcNow.AddDays(-20),
                                CustomerId = 2,
                                OrderDate = DateTimeOffset.UtcNow.AddDays(-20).AddHours(8),
                                Items = new List<OrderItem>()
                                {
                                    new OrderItem() {OrderId = 3, ProductId = 1, Quantity = 10, UnitPrice = 10},
                                    new OrderItem() {OrderId = 3, ProductId = 2, Quantity = 10, UnitPrice = 10},
                                    new OrderItem() {OrderId = 3, ProductId = 3, Quantity = 10, UnitPrice = 10},
                                    new OrderItem() {OrderId = 3, ProductId = 2, Quantity = 10, UnitPrice = 10},
                                    new OrderItem() {OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100}
                                },
                                Total = 250
                            },
                            new Order()
                            {
                                Id = 4,
                                CreatedAt = DateTimeOffset.UtcNow.AddDays(-3),
                                CustomerId = 3,
                                OrderDate = DateTimeOffset.UtcNow.AddDays(-3).AddHours(8),
                                Items = new List<OrderItem>()
                                {
                                    new OrderItem() {OrderId = 4, ProductId = 1, Quantity = 10, UnitPrice = 10},
                                    new OrderItem() {OrderId = 4, ProductId = 2, Quantity = 10, UnitPrice = 10},
                                    new OrderItem() {OrderId = 4, ProductId = 3, Quantity = 10, UnitPrice = 10},
                                    new OrderItem() {OrderId = 4, ProductId = 2, Quantity = 10, UnitPrice = 10},
                                    new OrderItem() {OrderId = 4, ProductId = 3, Quantity = 1, UnitPrice = 100}
                                },
                                Total = 850
                            }
                        );
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
