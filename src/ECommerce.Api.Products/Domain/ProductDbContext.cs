using ECommerce.Api.Products.Domain.Entities;
using ECommerce.Utilities.DomainHelpers;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Domain
{
    public class ProductDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public ProductDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();

            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();

                entity.HasOne(x => x.Category)
                    .WithMany(x => x.Products)
                    .HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
