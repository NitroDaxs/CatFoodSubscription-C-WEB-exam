using CatFoodSubscription.Data;
using CatFoodSubscription.Data.Models;
using CatFoodSubscription.Services.Data;
using CatFoodSubscription.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatFoodSubscription.Tests.ServicesTests
{
    [TestFixture]
    public class ProductServiceTests
    {

        private DbContextOptions<CatFoodSubscriptionDbContext> _options;
        private CatFoodSubscriptionDbContext dbContext;
        private IProductService productService;

        [SetUp]
        public async Task Setup()
        {
            _options = new DbContextOptionsBuilder<CatFoodSubscriptionDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            dbContext = new CatFoodSubscriptionDbContext(_options);
            productService = new ProductService(dbContext);

            await dbContext.Categories.AddRangeAsync(new List<Category>
            {
                new Category { Name = "Category 1" },
                new Category { Name = "Category 2" },
                new Category { Name = "Category 3" }
            });

            await dbContext.Products.AddRangeAsync(new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 10, IsDeleted = false,CategoryId = 1},
                new Product { Id = 2, Name = "Product 2", Price = 20, IsDeleted = false,CategoryId = 1 },
                new Product { Id = 3, Name = "Product 3", Price = 25, IsDeleted = false,CategoryId = 1 },
                new Product { Id = 4, Name = "Product 4", Price = 30, IsDeleted = false,CategoryId = 1 },
                new Product { Id = 5, Name = "Product 5", Price = 35, IsDeleted = false,CategoryId = 1 },
                new Product { Id = 6, Name = "Product 6", Price = 40, IsDeleted = false,CategoryId = 1 },
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
        public async Task GetProductAllAsync_Should_Return_All_Products()
        {
            var products = await productService.GetProductAllAsync();

            Assert.IsNotNull(products);
            Assert.AreEqual(6, products.Count());
        }


        [Test]
        public async Task GetProductSearchAsync_Should_Return_Products_From_Query()
        {
            var query1 = "pro".ToLower();

            var products = await productService.GetProductSearchAsync(query1);

            Assert.IsNotNull(products);
            Assert.AreEqual(6, products.Products.Count());
        }

        [Test]
        public async Task GetProductSearchAsync_Should_Throw_If_No_Products()
        {
            var query1 = "sadasdadassda".ToLower();

            Assert.ThrowsAsync<InvalidOperationException>(async () => await productService.GetProductSearchAsync(query1));
        }

        [Test]
        public async Task GetProductByIdAsync_Should_Return_Specified_Product_By_Id()
        {
            var products = await productService.GetProductByIdAsync(1);

            Assert.IsNotNull(products);
            Assert.AreEqual("Product 1", products.Name);
        }

        [Test]
        public async Task GetProductByIdAsync_Should_Throw_If_No_Product()
        {

            Assert.ThrowsAsync<InvalidOperationException>(async () => await productService.GetProductByIdAsync(-1));
        }

        [Test]
        public async Task GetProductSearchBarAsync_Should_Return_Products_From_Query_Search_Bar()
        {
            var products = await productService.GetProductSearchAsync("product");

            Assert.IsNotNull(products);
            Assert.AreEqual(6, products.Products.Count());
        }
    }
}
