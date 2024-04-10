using CatFoodSubscription.Data;
using CatFoodSubscription.Data.Models;
using CatFoodSubscription.Services.Data;
using CatFoodSubscription.Services.Data.Interfaces;
using CatFoodSubscription.Web.ViewModels.Admin.Order;
using CatFoodSubscription.Web.ViewModels.Admin.Product;
using Microsoft.EntityFrameworkCore;

namespace CatFoodSubscription.Tests.ServicesTests
{
    [TestFixture]
    public class AdminServiceTests
    {
        private DbContextOptions<CatFoodSubscriptionDbContext> _options;
        private CatFoodSubscriptionDbContext dbContext;
        private IAdminService adminService;
        private Customer customer;
        [SetUp]
        public async Task Setup()
        {
            _options = new DbContextOptionsBuilder<CatFoodSubscriptionDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            dbContext = new CatFoodSubscriptionDbContext(_options);
            customer = new Customer()
            {
                Id = "241994aa-d047-4340-b43f-69af93118c4b"
            };

            await dbContext.Products.AddRangeAsync(new List<Product>
            {
                new Product {Name = "Product 1", Price = 10 },
                new Product {Name = "Product 2", Price = 20 },
                new Product {Name = "Product 3", Price = 25 },
                new Product {Name = "Product 4", Price = 30 },
                new Product {Name = "Product 5", Price = 35 },
                new Product {Name = "Product 6", Price = 40 },
            });


            await dbContext.Categories.AddRangeAsync(new List<Category>
            {
                new Category { Name = "Category 1" },
                new Category { Name = "Category 2" },
                new Category { Name = "Category 3" }
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

            await dbContext.Statuses.AddRangeAsync(new List<Status>()
            {
                new Status { Name = "Test1" },
                new Status { Name = "Test2" },
                new Status { Name = "Test3" },
                new Status { Name = "Test4" },
            });

            await dbContext.Orders.AddRangeAsync(new List<Order>
            {
                new Order {CustomerId =  "241994aa-d047-4340-b43f-69af93118c4b",StatusId = 2,AddressId = 1},
                new Order {CustomerId =  "241994aa-d047-4340-b43f-69af93118c4b",StatusId = 3,AddressId = 2},
                new Order {CustomerId =  "241994aa-d047-4340-b43f-69af93118c4b",StatusId = 4,AddressId = 3},
            });

            await dbContext.Customers.AddAsync(customer);
            await dbContext.SaveChangesAsync();

            adminService = new AdminService(dbContext);
        }

        [TearDown]
        public async Task Teardown()
        {
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.DisposeAsync();
        }

        [Test]
        public async Task GetAdminProductBySearchAsync_Returns_Certain_Products()
        {
            var result = await adminService.GetAdminProductAllAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(6, result.Count());
        }

        [Test]
        public async Task GetAdminProductAllAsync_Returns_All_Products()
        {
            var result = await adminService.GetAdminProductBySearchAsync("product");

            Assert.IsNotNull(result);
            Assert.AreEqual(6, result.Count());

            var result2 = await adminService.GetAdminProductBySearchAsync("1");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result2.Count());

            var result3 = await adminService.GetAdminProductBySearchAsync("asddasda");
            Assert.AreEqual(0, result3.Count());
        }

        [Test]
        public async Task GetAdminProductByIdAsync_Returns_Certain_Product()
        {
            var result = await adminService.GetAdminProductByIdAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);

            var result2 = await adminService.GetAdminProductByIdAsync(6);

            Assert.IsNotNull(result);
            Assert.AreEqual(6, result2.Id);

            var result3 = await adminService.GetAdminProductByIdAsync(9999999);

            Assert.IsNull(result3);
        }

        [Test]
        public async Task GetAdminDeleteProductByIdAsync_Returns_Certain_Product()
        {
            var result = await adminService.GetAdminDeleteProductByIdAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);

            var result2 = await adminService.GetAdminDeleteProductByIdAsync(6);

            Assert.IsNotNull(result);
            Assert.AreEqual(6, result2.Id);

            var result3 = await adminService.GetAdminProductByIdAsync(9999999);

            Assert.IsNull(result3);
        }

        [Test]
        public async Task EditAdminProductByIdAsync_Throws_When_Model_Is_Null()
        {
            var model = new AdminProductEditViewModel();

            Assert.ThrowsAsync<InvalidOperationException>(async () => await adminService.EditAdminProductByIdAsync(model));

            var model2 = new AdminProductEditViewModel
            {
                Id = -1,
                Name = "Updated Product",
                Description = "Updated Description",
                Price = 20
            };
            Assert.ThrowsAsync<InvalidOperationException>(async () => await adminService.EditAdminProductByIdAsync(model2));

            var model3 = new AdminProductEditViewModel
            {
                Name = null,
            };
            Assert.ThrowsAsync<InvalidOperationException>(async () => await adminService.EditAdminProductByIdAsync(model3));
        }

        [Test]
        public async Task EditAdminProductByIdAsync_Returns_When_UpdatedProduct()
        {
            var model = new AdminProductEditViewModel
            {
                Id = 6,
                Name = "Updated Product",
                Description = "Updated Description",
                Price = 20
            };

            await adminService.EditAdminProductByIdAsync(model);

            var updatedProduct = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == model.Id);
            Assert.IsNotNull(updatedProduct);
            Assert.AreEqual(model.Name, updatedProduct.Name);
            Assert.AreEqual(model.Description, updatedProduct.Description);
            Assert.AreEqual(model.Price, updatedProduct.Price);
        }

        [Test]
        public async Task ConfirmAdminDeleteProductAsync_Throws_Error_When_Model_Is_Null()
        {
            var model = new AdminDeleteViewModel();

            Assert.ThrowsAsync<InvalidOperationException>(async () => await adminService.ConfirmAdminDeleteProductAsync(model));

            var model2 = new AdminDeleteViewModel
            {
                Id = -1,
            };
            Assert.ThrowsAsync<InvalidOperationException>(async () => await adminService.ConfirmAdminDeleteProductAsync(model2));
        }

        [Test]
        public async Task ConfirmAdminDeleteProductAsync_Soft_Deletes_Product()
        {
            var model = new AdminDeleteViewModel()
            {
                Id = 1
            };

            await adminService.ConfirmAdminDeleteProductAsync(model);

            var products = dbContext.Products.ToList().Where(p => p.IsDeleted == false);

            Assert.IsNotNull(products);
            Assert.AreEqual(5, products.Count());
        }

        [Test]
        public async Task GetAdminRestoreProductByIdAsync_Returns_Certain_Product()
        {
            var result = await adminService.GetAdminRestoreProductByIdAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);

            var result2 = await adminService.GetAdminRestoreProductByIdAsync(6);

            Assert.IsNotNull(result);
            Assert.AreEqual(6, result2.Id);

            var result3 = await adminService.GetAdminRestoreProductByIdAsync(9999999);

            Assert.IsNull(result3);
        }

        [Test]
        public async Task ConfirmAdminRestoreProductAsync_Throws_Exception_If_Model_Is_Null()
        {
            var model = new AdminRestoreViewModel();

            Assert.ThrowsAsync<InvalidOperationException>(async () => await adminService.ConfirmAdminRestoreProductAsync(model));

            var model2 = new AdminRestoreViewModel
            {
                Id = -1,
            };
            Assert.ThrowsAsync<InvalidOperationException>(async () => await adminService.ConfirmAdminRestoreProductAsync(model2));
        }

        [Test]
        public async Task ConfirmAdminRestoreProductAsync_Restores_Product()
        {
            var model = new AdminRestoreViewModel()
            {
                Id = 1,
            };

            await adminService.ConfirmAdminRestoreProductAsync(model);

            var products = await dbContext.Products.ToListAsync();

            Assert.IsNotNull(products);
            Assert.AreEqual(6, products.Count());
        }

        [Test]
        public async Task GetAdminProductCategoriesAsync_Returns_All_Categories()
        {
            var categories = await adminService.GetAdminProductCategoriesAsync();

            Assert.IsNotNull(categories);
            Assert.AreEqual(3, categories.Count());
        }

        [Test]
        public async Task AddNewProductAsync_Adds_New_Product()
        {
            var newProductViewModel = new AdminAddProductViewModel
            {
                Name = "New Product",
                Description = "Description of New Product",
                Price = 20,
                IsSubscription = false,
                CategoryId = 1
            };

            await adminService.AddNewProductAsync(newProductViewModel);

            var addedProduct = dbContext.Products.FirstOrDefault(p => p.Name == "New Product");

            Assert.IsNotNull(addedProduct);
            Assert.AreEqual(20, addedProduct.Price);

            var productCount = await adminService.GetAdminProductAllAsync();

            Assert.IsNotNull(productCount);
            Assert.AreEqual(7, productCount.Count());
        }

        [Test]
        public async Task GetAdminOrderAllAsync_Returns_All_Orders()
        {
            var orders = await adminService.GetAdminOrderAllAsync();

            Assert.IsNotNull(orders);
            Assert.AreEqual(3, orders.Count());
        }


        [Test]
        public async Task GetAdminOrderByIdAsyncc_Returns_Certain_Order()
        {
            var order = await adminService.GetAdminOrderByIdAsync(1);

            Assert.IsNotNull(order);
            Assert.AreEqual(1, order.FirstOrDefault().Id);

            var order2 = await adminService.GetAdminOrderByIdAsync(2);

            Assert.IsNotNull(order2);
            Assert.AreEqual(2, order2.FirstOrDefault().Id);

            var order3 = await adminService.GetAdminOrderByIdAsync(1123123);
            Assert.IsEmpty(order3);
        }

        [Test]
        public async Task GetAdminOrderByIdChangeStatusAsync_Returns_Certain_Order()
        {
            var order = await adminService.GetAdminOrderByIdChangeStatusAsync(1);

            Assert.IsNotNull(order);
            Assert.AreEqual(1, order.Id);

            var order2 = await adminService.GetAdminOrderByIdChangeStatusAsync(2);

            Assert.IsNotNull(order2);
            Assert.AreEqual(2, order2.Id);

            var order3 = await adminService.GetAdminOrderByIdChangeStatusAsync(1123123);

            Assert.IsNull(order3);
        }

        [Test]
        public async Task UpdateAdminOrderStatus_Throws_Exception_If_Model_Is_Null()
        {
            var model = new AdminOrderChangeStatusViewModel();

            Assert.ThrowsAsync<InvalidOperationException>(async () => await adminService.UpdateAdminOrderStatus(model));

            var model2 = new AdminOrderChangeStatusViewModel()
            {
                CustomerId = customer.Id,
                StatusId = 0
            };

            Assert.ThrowsAsync<InvalidOperationException>(async () => await adminService.UpdateAdminOrderStatus(model2));
        }

        [Test]
        public async Task UpdateAdminOrderStatus_Updates_Status()
        {
            var model = new AdminOrderChangeStatusViewModel()
            {
                Id = 1,
                CustomerId = customer.Id,
                StatusId = 4
            };

            await adminService.UpdateAdminOrderStatus(model);

            var updatedOrder = await dbContext.Orders.Where(o => o.Id == model.Id).FirstOrDefaultAsync();

            Assert.IsNotNull(updatedOrder);
            Assert.AreEqual(4, updatedOrder.StatusId);
        }

        [Test]
        public async Task GetAdminOrderStatusesAsync_Returns_Statuses()
        {
            var statuses = await adminService.GetAdminOrderStatusesAsync();

            Assert.IsNotNull(statuses);
            Assert.AreEqual(3, statuses.Count());
        }

        [Test]
        public async Task OrderSummaryByIdAsync_Returns_Certain_Order()
        {
            var order = await adminService.OrderSummaryByIdAsync(2);
            Assert.IsNotNull(order);
            Assert.AreEqual("Kon2", order.Address.FirstName);
        }
    }
}