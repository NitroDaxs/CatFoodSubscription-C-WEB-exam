using CatFoodSubscription.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CatFoodSubscription.Data
{
    public class CatFoodSubscriptionDbContext : IdentityDbContext
    {
        public CatFoodSubscriptionDbContext(DbContextOptions<CatFoodSubscriptionDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Subscription> Subscriptions { get; set; } = null!;
        public DbSet<SubscriptionBox> SubscriptionBoxes { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
    }
}
