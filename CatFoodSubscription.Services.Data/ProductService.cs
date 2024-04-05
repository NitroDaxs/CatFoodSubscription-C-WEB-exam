using CatFoodSubscription.Data;
using CatFoodSubscription.Services.Data.Interfaces;
using CatFoodSubscription.Web.ViewModels.Product;
using Microsoft.EntityFrameworkCore;

namespace CatFoodSubscription.Services.Data
{
    public class ProductService : IProductService
    {
        private readonly CatFoodSubscriptionDbContext context;

        public ProductService(CatFoodSubscriptionDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<ProductAllViewModel>> GetProductAllAsync()
        {
            var products = await context.Products
                .AsNoTracking()
                .Where(p => p.IsDeleted == false)
                .Select(p => new ProductAllViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    Category = p.Category.Name,
                })
                .ToListAsync();


            if (!products.Any())
            {
                throw new NullReferenceException();
            }

            return products;
        }

        public async Task<PaginatedProductsViewModel> GetProductSearchAsync(string query)
        {

            var products = await context.Products
                .AsNoTracking()
                .Where(p => p.Name.Contains(query))
                .Select(p => new ProductAllViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    Category = p.Category.Name,
                })
                .ToListAsync();

            var result = new PaginatedProductsViewModel()
            {
                Products = products
            };

            if (!result.Products.Any())
            {
                throw new NullReferenceException();
            }

            return result;
        }

        public async Task<ProductDetailViewModel> GetProductByIdAsync(int id)
        {
            var product = await context.Products
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new ProductDetailViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    Description = p.Description,
                    Category = p.Category.Name,
                })
                .FirstOrDefaultAsync();

            if (product == null)
            {
                throw new Exception();
            }

            return product;
        }

        public async Task<IEnumerable<ProductSearchViewModel>> GetProductSearchBarAsync(string query)
        {
            var results = await context.Products
                .Where(p => p.Name.Contains(query))
                .Select(p => new ProductSearchViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl
                })
                .ToListAsync();

            if (results == null)
            {
                throw new NullReferenceException();
            }

            return results;
        }
    }
}
