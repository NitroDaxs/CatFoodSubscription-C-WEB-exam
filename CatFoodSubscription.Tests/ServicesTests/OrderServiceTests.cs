using CatFoodSubscription.Data;
using CatFoodSubscription.Data.Models;
using CatFoodSubscription.Services.Data;
using CatFoodSubscription.Services.Data.Interfaces;
using CatFoodSubscription.Web.ViewModels.Order;
using CatFoodSubscription.Web.ViewModels.SubscriptionBox;
using Microsoft.EntityFrameworkCore;

namespace CatFoodSubscription.Tests.ServicesTests
{
    [TestFixture]
    public class OrderServiceTests
    {
        private DbContextOptions<CatFoodSubscriptionDbContext> _options;
        private CatFoodSubscriptionDbContext dbContext;
        private IOrderService orderService;

        [SetUp]
        public async Task Setup()
        {
            _options = new DbContextOptionsBuilder<CatFoodSubscriptionDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            dbContext = new CatFoodSubscriptionDbContext(_options);

            await dbContext.Orders.AddRangeAsync(new List<Order>
            {
                new Order { Id = 1, CustomerId = "user1", StatusId = 3 ,AddressId = 1},
                new Order { Id = 2, CustomerId = "user2", StatusId = 2 ,AddressId = 2 },
                new Order { Id = 3, CustomerId = "user3", StatusId = 2,AddressId = 2},
                new Order { Id = 4, CustomerId = "user1", StatusId = 1,SubscriptionBoxId = 1},
                new Order { Id = 5, CustomerId = "user1", StatusId = 3 , AddressId = 3},
            });

            await dbContext.Customers.AddRangeAsync(new List<Customer>()
            {
                new Customer{Id = "user1"},
                new Customer{Id = "user2"},
                new Customer{Id = "user3"},
                new Customer{Id = "user4"},
            });

            await dbContext.Statuses.AddRangeAsync(new List<Status>()
            {
                new Status { Name = "Test1" },
                new Status { Name = "Test2" },
                new Status { Name = "Test3" },
                new Status { Name = "Test4" },
            });

            await dbContext.ProductsOrders.AddRangeAsync(new List<ProductOrder>
            {
                new ProductOrder { OrderId = 1, ProductId = 1, Quantity = 2 },
                new ProductOrder { OrderId = 4, ProductId = 1, Quantity = 2 },
                new ProductOrder { OrderId = 4, ProductId = 2, Quantity = 1 },
                new ProductOrder { OrderId = 2, ProductId = 2, Quantity = 1 },
            });

            await dbContext.Products.AddRangeAsync(new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 10 ,ImageUrl = "Url1"},
                new Product { Id = 2, Name = "Product 2", Price = 20,ImageUrl = "Url2" },
            });

            await dbContext.SubscriptionBoxes.AddRangeAsync(new List<SubscriptionBox>()
            {
                new SubscriptionBox { Id = 1, Name = "Box 1", Price = 20,Description = "Descrption 1" ,ImageUrl = "Url1"},
                new SubscriptionBox { Id = 2, Name = "Box 2", Price = 30 ,Description = "Descrption 2",ImageUrl = "Url2"},
                new SubscriptionBox { Id = 3, Name = "Box 2", Price = 40 ,Description = "Descrption 3",ImageUrl = "Url3"}
            });
            await dbContext.Addresses.AddRangeAsync(new List<Address>()
            {
                new Address
                {
                    FirstName = "Kon1", LastName = "Konev1", PhoneNumber = "0000000001", Email = "kon@test1.com",
                    Country = "Test1", City = "Sredets", PostalCode = "8300", Street = "Street1"
                },
                new Address
                {
                    FirstName = "Kon2", LastName = "Konev2", PhoneNumber = "0000000002", Email = "kon@test2.com",
                    Country = "Test2", City = "Sredets", PostalCode = "8300", Street = "Street2"
                },
                new Address
                {
                    FirstName = "Kon3", LastName = "Konev3", PhoneNumber = "0000000003", Email = "kon@test3.com",
                    Country = "Test3", City = "Sredets", PostalCode = "8300", Street = "Street3"
                }
            });
            await dbContext.SaveChangesAsync();

            orderService = new OrderService(dbContext);
        }
        [TearDown]
        public async Task Teardown()
        {
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.DisposeAsync();
        }

        [Test]
        public async Task GetOrderSummaryAsync_Should_Return_OrderSummary_For_Pending_Order()
        {
            string userId = "user1";

            var result = await orderService.GetOrderSummaryAsync(userId);

            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Id);
        }

        [Test]
        public async Task GetOrderSummaryAsync_Should_Create_New_Order_For_User_If_No_Pending_Order_Exists()
        {
            string userId = "user3";

            var result = await orderService.GetOrderSummaryAsync(userId);

            Assert.IsNotNull(result);
            Assert.AreEqual(userId, result.CustomerId);
            Assert.AreEqual(6, result.Id);
            Assert.IsNotNull(result.Id);
        }

        [Test]
        public async Task GetProductByIdAsync_Should_Return_Product_By_Id()
        {
            int productId = 1;

            var result = await orderService.GetProductByIdAsync(productId);

            Assert.IsNotNull(result);
            Assert.AreEqual(productId, result.Id);
        }

        [Test]
        public async Task GetCheckOutSummaryAsync_Should_Return_CheckOutSummary_For_User()
        {
            string userId = "user1";

            var result = await orderService.GetCheckOutSummaryAsync(userId);

            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.OrderId);
            Assert.AreEqual(2, result.Products.Count());
        }

        [Test]
        public async Task ProcessOrderAsync_Should_Process_Order_And_Save_Address()
        {
            var model = new OrderCheckOutFormViewModel
            {
                OrderId = 1,
                Address = new OrderAddressViewModel
                {
                    Country = "Country",
                    City = "City",
                    Street = "Street",
                    PostalCode = "12345",
                    PhoneNumber = "1234567890",
                    FirstName = "FirstName",
                    LastName = "LastName",
                    Email = "test@example.com"
                }
            };
            string userId = "user1";

            await orderService.ProcessOrderAsync(model, userId);

            var order = await dbContext.Orders.Include(o => o.Address).FirstOrDefaultAsync(o => o.Id == model.OrderId);
            Assert.IsNotNull(order);
            Assert.AreEqual(model.Address.Country, order.Address.Country);
            Assert.AreEqual(model.Address.City, order.Address.City);
            Assert.AreEqual(model.Address.Street, order.Address.Street);
            Assert.AreEqual(model.Address.PostalCode, order.Address.PostalCode);
            Assert.AreEqual(model.Address.PhoneNumber, order.Address.PhoneNumber);
            Assert.AreEqual(model.Address.FirstName, order.Address.FirstName);
            Assert.AreEqual(model.Address.LastName, order.Address.LastName);
            Assert.AreEqual(model.Address.Email, order.Address.Email);
            Assert.AreEqual(2, order.StatusId);
        }

        [Test]
        public async Task AddToCartAsync_Should_Add_Product_To_Cart()
        {
            var product = new OrderProductViewModel { Id = 2, Name = "Product 2", Price = 20 };
            string userId = "user1";

            await orderService.AddToCartAsync(product, userId);

            var order = await dbContext.Orders.Include(o => o.ProductsOrders).FirstOrDefaultAsync(o => o.CustomerId == userId && o.StatusId == 1);
            Assert.IsNotNull(order);
            var productOrder = order.ProductsOrders.FirstOrDefault(po => po.ProductId == product.Id);
            Assert.IsNotNull(productOrder);
            Assert.AreEqual(2, productOrder.Quantity);
        }

        [Test]
        public async Task UpdateProductQuantityAsync_Should_Increase_Or_Decrease_Product_Quantity_In_Cart()
        {
            int productId = 1;
            string actionIncrease = "increase";
            string actionDecrease = "decrease";
            string userId = "user1";

            await orderService.UpdateProductQuantityAsync(productId, actionIncrease, userId);
            await orderService.UpdateProductQuantityAsync(productId, actionIncrease, userId);
            await orderService.UpdateProductQuantityAsync(productId, actionDecrease, userId);

            var order = await dbContext.Orders.Include(o => o.ProductsOrders).FirstOrDefaultAsync(o => o.CustomerId == userId && o.StatusId == 1);
            Assert.IsNotNull(order);
            var productOrder = order.ProductsOrders.FirstOrDefault(po => po.ProductId == productId);
        }

        [Test]
        public async Task AddSubscriptionBoxToCartAsync_Should_Add_SubscriptionBox_To_Cart()
        {
            var box1 = await dbContext.SubscriptionBoxes.FindAsync(1);
            var subscriptionBox = new SubscriptionBoxAllViewModel { Id = box1.Id, Name = box1.Name, Price = box1.Price };
            string userId = "user1";

            dbContext.Entry(box1).State = EntityState.Detached;

            await orderService.AddSubscriptionBoxToCartAsync(subscriptionBox, userId);

            var order = await dbContext.Orders.Include(o => o.SubscriptionBox)
                .FirstOrDefaultAsync(o => o.CustomerId == userId && o.StatusId == 1);
            Assert.IsNotNull(order);
            Assert.IsNotNull(order.SubscriptionBox);
            Assert.AreEqual(subscriptionBox.Id, order.SubscriptionBox.Id);
        }

        [Test]
        public async Task RemoveSubscriptionBoxAsync_Should_Remove_SubscriptionBox_From_Cart()
        {
            int orderId = 1;

            await orderService.RemoveSubscriptionBoxAsync(orderId);

            var order = await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            Assert.IsNotNull(order);
            Assert.IsNull(order.SubscriptionBoxId);
        }

        [Test]
        public async Task GetAllOrdersByIdAsync_Should_Return_All_Orders_For_User()
        {
            string userId = "user2";

            var result = await orderService.GetAllOrdersByIdAsync(userId);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public async Task OrderSummaryAsync_Should_Return_OrderSummary_By_Id()
        {
            int orderId = 2;

            var result = await orderService.OrderSummaryAsync(orderId);

            Assert.IsNotNull(result);
            Assert.AreEqual(orderId, result.Id);
        }

        [Test]
        public async Task OrderSummaryAsync_Should_Throw_Exception_If_Order_Not_Found()
        {
            int orderId = -1;

            Assert.ThrowsAsync<NullReferenceException>(async () => await orderService.OrderSummaryAsync(orderId));
        }
    }
}
