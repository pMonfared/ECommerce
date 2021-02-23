using ECommerce.Api.Orders.Domain.Entities;
using ECommerce.Utilities.DomainHelpers;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.Domain
{
    public class OrderDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public OrderDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();

            });
            
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();

                entity.HasOne(x => x.Order)
                    .WithMany(x => x.Items)
                    .HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}
