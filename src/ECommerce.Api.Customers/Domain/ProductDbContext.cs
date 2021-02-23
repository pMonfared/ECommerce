using ECommerce.Api.Customers.Domain.Entities;
using ECommerce.Utilities.DomainHelpers;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Domain
{
    public class CustomerDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomerDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();

            });

        }
    }
}
