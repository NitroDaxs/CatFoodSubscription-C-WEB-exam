using CatFoodSubscription.Data;
using CatFoodSubscription.Data.Models;
using CatFoodSubscription.Services.Data;
using CatFoodSubscription.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatFoodSubscription.Tests.ServicesTests
{
    [TestFixture]
    public class SubscriptionServiceTests
    {
        private DbContextOptions<CatFoodSubscriptionDbContext> _options;
        private CatFoodSubscriptionDbContext dbContext;
        private ISubscriptionService subscriptionService;

        [SetUp]
        public async Task Setup()
        {
            _options = new DbContextOptionsBuilder<CatFoodSubscriptionDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            dbContext = new CatFoodSubscriptionDbContext(_options);
            subscriptionService = new SubscriptionService(dbContext);

            await dbContext.AddRangeAsync(new List<SubscriptionBox>()
            {
                new SubscriptionBox { Id = 1, Name = "Box 1", Price = 20,Description = "Descrption 1"},
                new SubscriptionBox { Id = 2, Name = "Box 2", Price = 30 ,Description = "Descrption 2"}
            })
            ;

            await dbContext.AddRangeAsync(new List<Product>()
            {
                new Product { Id = 1, Name = "Product 1", Price = 10, IsSubscription = true },
                new Product { Id = 2, Name = "Product 2", Price = 15, IsSubscription = true }
            });

            await dbContext.AddRangeAsync(new List<Order>()
            {
                new Order
                {
                    Id = 1,
                    CustomerId = "customer1",
                    IsSubscriptionCanceled = false,
                    StatusId = 2, // Assuming status 2 indicates active subscription
                    SubscriptionBoxId = 1, // Assuming the first order is associated with Box 1
                    ProductsOrders = new List<ProductOrder>
                    {
                        new ProductOrder { ProductId = 1, Quantity = 1 },
                        new ProductOrder { ProductId = 2, Quantity = 2 }
                    }
                },
                new Order
                {
                    Id = 2,
                    CustomerId = "customer1",
                    IsSubscriptionCanceled = false,
                    StatusId = 2, // Assuming status 2 indicates active subscription
                    SubscriptionBoxId = 2, // Assuming the second order is associated with Box 2
                    ProductsOrders = new List<ProductOrder>
                    {
                        new ProductOrder { ProductId = 1, Quantity = 1 }
                    }
                },
                new Order
                {
                    Id = 3,
                    CustomerId = "customer2",
                    IsSubscriptionCanceled = false,
                    StatusId = 2, // Assuming status 2 indicates active subscription
                    SubscriptionBoxId = 1, // Assuming the third order is associated with Box 1
                    ProductsOrders = new List<ProductOrder>
                    {
                        new ProductOrder { ProductId = 2, Quantity = 2 }
                    }
                }
            });

            await dbContext.SaveChangesAsync();
        }

        [TearDown]
        public async Task Teardown()
        {
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.DisposeAsync();
        }

        [Test]
        public async Task GetOrderSubscriptionProductAsync_Returns_All_Subscription_Orders()
        {
            var result = await subscriptionService.GetOrderSubscriptionProductAsync("customer1");
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Orders.Count());
        }

        [Test]
        public async Task CancelSubscription_Fetches_And_Cancels_Subscription()
        {
            await subscriptionService.CancelSubscription(1);

            var cancelledOrder = await dbContext.Orders.FindAsync(1);

            Assert.IsNotNull(cancelledOrder);
            Assert.IsTrue(cancelledOrder.IsSubscriptionCanceled);
        }


        [Test]
        public async Task CancelSubscription_If_Order_Is_Null_Throws()
        {
            Assert.ThrowsAsync<InvalidOperationException>(async () => await subscriptionService.CancelSubscription(-1));
        }
    }
}
