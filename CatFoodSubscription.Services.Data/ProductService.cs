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

        /// <summary>
        /// This service is responsible for fetching all available products.
        /// </summary>
        /// <returns>A collection of all available products.</returns>
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

        /// <summary>
        /// This service is responsible for searching for products based on the provided query.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <returns>A paginated view model containing the search results.</returns>
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

        /// <summary>
        /// This service is responsible for retrieving product details by its Id.
        /// </summary>
        /// <param name="id">The Id of the product.</param>
        /// <returns>The detailed information of the specified product.</returns>
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

        /// <summary>
        /// This service is responsible for searching for products based on the provided query for the search bar.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <returns>A collection of products matching the search query.</returns>
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
