using CatFoodSubscription.Data;
using CatFoodSubscription.Web.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatFoodSubscription.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly CatFoodSubscriptionDbContext dbContext;

        public ProductController(CatFoodSubscriptionDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        //Returns all products
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await dbContext.Products
                .AsNoTracking()
                .Select(p => new ProductAllViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    Category = p.Category.Name,
                })
                .ToListAsync();


            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var product = await dbContext.Products
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new ProductDetailViewModel()
                {
                    Id = id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    Description = p.Description,
                    Category = p.Category.Name,
                })
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
