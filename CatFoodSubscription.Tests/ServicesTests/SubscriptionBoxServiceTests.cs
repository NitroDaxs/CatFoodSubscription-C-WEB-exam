using CatFoodSubscription.Data;
using CatFoodSubscription.Data.Models;
using CatFoodSubscription.Services.Data;
using CatFoodSubscription.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatFoodSubscription.Tests.ServicesTests
{
    [TestFixture]
    public class SubscriptionBoxServiceTests
    {
        private DbContextOptions<CatFoodSubscriptionDbContext> _options;
        private CatFoodSubscriptionDbContext dbContext;
        private ISubscriptionBoxService subscriptionBoxService;

        [SetUp]
        public async Task Setup()
        {
            _options = new DbContextOptionsBuilder<CatFoodSubscriptionDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            dbContext = new CatFoodSubscriptionDbContext(_options);
            await dbContext.Products.AddRangeAsync(new List<Product>
            {
                new Product {Name = "Product 1", Price = 10 },
                new Product {Name = "Product 2", Price = 20 },
                new Product {Name = "Product 3", Price = 25 },
                new Product {Name = "Product 4", Price = 30 },
                new Product {Name = "Product 5", Price = 35 },
                new Product {Name = "Product 6", Price = 40 },
            });

            await dbContext.AddRangeAsync(new List<SubscriptionBox>()
            {
                new SubscriptionBox { Id = 1, Name = "Box 1", Price = 20,Description = "Descrption 1"},
                new SubscriptionBox { Id = 2, Name = "Box 2", Price = 30 ,Description = "Descrption 2"},
                new SubscriptionBox { Id = 3, Name = "Box 2", Price = 40 ,Description = "Descrption 3"}
            });

            await dbContext.SaveChangesAsync();

            subscriptionBoxService = new SubscriptionBoxService(dbContext);
        }

        [TearDown]
        public async Task Teardown()
        {
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.DisposeAsync();
        }

        [Test]
        public async Task GetAllAsync_Should_Return_All_SubscriptionBoxes()
        {
            var result = await subscriptionBoxService.GetAllAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }
    }
}
