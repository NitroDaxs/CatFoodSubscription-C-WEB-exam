using CatFoodSubscription.Data.Models;
using CatFoodSubscription.Data.SeedDb;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CatFoodSubscription.Data
{
    public class CatFoodSubscriptionDbContext : IdentityDbContext<Customer, IdentityRole<string>, string>
    {
        public CatFoodSubscriptionDbContext(DbContextOptions<CatFoodSubscriptionDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductSubscriptionBox>(entity =>
            {
                entity.HasKey(ps => new { ps.ProductId, ps.SubscriptionBoxId });

                entity.HasOne(ps => ps.Product)
                    .WithMany(p => p.ProductSubscriptionBoxes)
                    .HasForeignKey(ps => ps.ProductId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(ps => ps.SubscriptionBox)
                    .WithMany(s => s.ProductSubscriptionBoxes)
                    .HasForeignKey(ps => ps.SubscriptionBoxId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<ProductOrder>(entity =>
            {
                entity.HasKey(po => new { po.OrderId, po.ProductId });

                entity.HasOne(po => po.Product)
                    .WithMany(p => p.ProductsOrders)
                    .HasForeignKey(po => po.ProductId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(po => po.Order)
                    .WithMany(o => o.ProductsOrders)
                    .HasForeignKey(o => o.OrderId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<SubscriptionProductOrder>(entity =>
            {
                entity.HasKey(po => new { po.OrderId, po.ProductId });

                entity.HasOne(po => po.Product)
                    .WithMany(p => p.SubscriptionProductsOrders)
                    .HasForeignKey(po => po.ProductId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(po => po.Order)
                    .WithMany(o => o.SubscriptionProductsOrders)
                    .HasForeignKey(o => o.OrderId)
                    .OnDelete(DeleteBehavior.NoAction);
            });


            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new StatusConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionBoxConfiguration());
            modelBuilder.ApplyConfiguration(new ProductSubscriptionBoxConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<SubscriptionBox> SubscriptionBoxes { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Status> Statuses { get; set; } = null!;
        public DbSet<ProductOrder> ProductsOrders { get; set; } = null!;
        public DbSet<SubscriptionProductOrder> SubscriptionProductsOrders { get; set; } = null!;
        public DbSet<ProductSubscriptionBox> ProductSubscriptionBoxes { get; set; } = null!;
    }
}
