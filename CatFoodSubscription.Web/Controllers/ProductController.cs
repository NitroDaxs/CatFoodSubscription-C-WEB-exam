using CatFoodSubscription.Services.Data.Interfaces;
using CatFoodSubscription.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatFoodSubscription.Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService productService;

        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }

        //Returns all products
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string category, string sortOrder)
        {
            var products = await productService.GetProductAllAsync();

            if (!products.Any())
            {
                return BadRequest();
            }

            products = SortProducts(category, sortOrder, products);

            return View(products);
        }

        //Returns all valid entities from search bar
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> SearchResult(string query)
        {
            var results = await productService.GetProductSearchAsync(query);

            if (!results.Any())
            {
                return RedirectToAction("Index");
            }

            return View("Index", results);
        }


        //Button "Details" showing product detail by product Id
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Detail(int id)
        {
            var product = await productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        //Result for dropdown search bar products
        [HttpGet("/api/search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search(string query)
        {
            var results = await productService.GetProductSearchBarAsync(query);

            return Json(results);
        }


        private static IEnumerable<ProductAllViewModel> SortProducts(string category, string sortOrder, IEnumerable<ProductAllViewModel> products)
        {
            if (!string.IsNullOrEmpty(category))
            {
                // Filter by category
                products = products.Where(p => p.Category == category);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case "price_asc":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                default:
                    products = products.OrderBy(p => p.Name);
                    break;
            }

            return products;
        }
    }
}
