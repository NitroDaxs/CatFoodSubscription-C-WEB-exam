using CatFoodSubscription.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatFoodSubscription.Data.SeedDb
{
    internal class ProductSubscriptionBoxConfiguration : IEntityTypeConfiguration<ProductSubscriptionBox>
    {
        public void Configure(EntityTypeBuilder<ProductSubscriptionBox> builder)
        {
            var data = new SeedData();

            builder.HasData(data.GetProductSubscriptionBoxes());
        }
    }
}
