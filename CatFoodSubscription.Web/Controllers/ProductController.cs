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
        public async Task<IActionResult> Index(string category, string sortOrder, int page = 1)
        {
            const int pageSize = 8;
            try
            {
                var products = await productService.GetProductAllAsync();

                products = SortProducts(category, sortOrder, products);

                var paginatedProducts = products.Skip((page - 1) * pageSize).Take(pageSize);

                var viewModel = new PaginatedProductsViewModel
                {
                    Products = paginatedProducts,
                    PaginationInfo = new ProductListViewModel()
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = products.Count()
                    }
                };

                // Set ViewBag properties for category and sortOrder if needed
                ViewBag.Category = category;
                ViewBag.SortOrder = sortOrder;

                return View(viewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "SubscriptionBox");
            }

        }

        //Returns all valid entities from search bar
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> SearchResult(string query)
        {
            try
            {
                var result = await productService.GetProductSearchAsync(query);
                return View("Index", result);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Product");
            }
        }


        //Button "Details" showing product detail by product Id
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var product = await productService.GetProductByIdAsync(id);

                return View(product);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { statusCode = 404 });
            }
        }

        //Result for dropdown search bar products
        [HttpGet("/api/search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search(string query)
        {
            var results = await productService.GetProductSearchBarAsync(query);

            return Json(results);
        }


        //Product sorting method
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
