using CatFoodSubscription.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatFoodSubscription.Data.SeedDb
{
    internal class SubscriptionBoxConfiguration : IEntityTypeConfiguration<SubscriptionBox>
    {
        public void Configure(EntityTypeBuilder<SubscriptionBox> builder)
        {
            builder
                .Property(sb => sb.Price)
                .HasColumnType("decimal(18, 2)");

            var data = new SeedData();

            builder.HasData(data.GetSubscriptionBoxes());
        }
    }
}
