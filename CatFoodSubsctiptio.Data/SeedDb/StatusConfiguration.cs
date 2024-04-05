using CatFoodSubscription.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatFoodSubscription.Data.SeedDb
{
    internal class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            var data = new SeedData();

            builder.HasData(data.GetStatuses());
        }
    }
}
