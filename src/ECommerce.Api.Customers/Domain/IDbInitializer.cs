using ECommerce.Api.Customers.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ECommerce.Api.Customers.Domain
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
            using var context = serviceScope.ServiceProvider.GetService<CustomerDbContext>();
            context.Database.EnsureCreated();
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<CustomerDbContext>())
                {

                    //add admin user
                    if (!context.Customers.Any())
                    {

                        context.Customers.AddRange(
                            new Customer()
                            {
                                Id = 1,
                                CreatedAt = DateTimeOffset.UtcNow.AddDays(-4),
                                FirstName = "Pooria",
                                LastName = "Monfared",
                                Email = "poriya.monfared@gmail.com",
                                PhoneNumber = "+132456789",
                                Address = "TR, Istanbul"
                            },
                    new Customer()
                    {
                        Id = 2,
                        CreatedAt = DateTimeOffset.UtcNow.AddDays(-10),
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "John@gmail.com",
                        PhoneNumber = "+132456789",
                        Address = "LA, North 3"
                    },
                    new Customer()
                    {
                        Id = 3,
                        CreatedAt = DateTimeOffset.UtcNow.AddDays(-20),
                        FirstName = "Angela",
                        LastName = "Gorgi",
                        Email = "AngelaGorgi@gmail.com",
                        PhoneNumber = "+132456789",
                        Address = "CA, South 6"
                    },
                    new Customer()
                    {
                        Id = 4,
                        CreatedAt = DateTimeOffset.UtcNow.AddDays(-3),
                        FirstName = "Soor",
                        LastName = "Dar",
                        Email = "SoorDar@gmail.com",
                        PhoneNumber = "+132456789",
                        Address = "IR, North 100"
                    }
                            );
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
