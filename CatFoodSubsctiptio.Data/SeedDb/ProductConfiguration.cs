using CatFoodSubscription.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatFoodSubscription.Data.SeedDb
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)");

            var data = new SeedData();

            builder.HasData(data.GetProducts());
        }
    }
}
