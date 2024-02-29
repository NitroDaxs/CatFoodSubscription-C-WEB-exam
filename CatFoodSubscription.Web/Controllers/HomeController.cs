using CatFoodSubscription.Data;
using CatFoodSubscription.Web.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CatFoodSubscription.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CatFoodSubscriptionDbContext dbContext;

        public HomeController(ILogger<HomeController> logger, CatFoodSubscriptionDbContext _dbContext)
        {
            _logger = logger;
            dbContext = _dbContext;
        }

        //Home page with all subscriptionBoxes
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var subscriptionBoxes = await dbContext.SubscriptionBoxes
                .Include(sb => sb.ProductSubscriptionBoxes)
                .ThenInclude(psb => psb.Product)
                .AsNoTracking()
                .ToListAsync();

            var model = subscriptionBoxes
                .Select(sb => new SubscriptionBoxAllViewModel()
                {
                    Id = sb.Id,
                    Name = sb.Name,
                    ImageUrl = sb.ImageUrl,
                    Description = sb.Description,
                    Price = sb.Price,
                    ProductSubscriptionBoxes = sb.ProductSubscriptionBoxes
                        .Select(psb => new ProductSubscriptionBoxViewModel()
                        {
                            ProductId = psb.ProductId,
                            ProductName = psb.Product.Name,
                        })
                        .ToList()
                })
                .ToList();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
