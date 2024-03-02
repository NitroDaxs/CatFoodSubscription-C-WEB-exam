using CatFoodSubscription.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CatFoodSubscription.Web.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;


        public OrderController(IOrderService _orderService)
        {
            orderService = _orderService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var summary = await orderService.GetOrderSummaryAsync(GetUserId());

            return View(summary);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int productId, string action)
        {
            await orderService.UpdateProductQuantityAsync(productId, action, GetUserId());

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> CheckOut()
        {
            return View();
        }

        // Action to add a product to the cart
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, bool purchaseType)
        {
            var product = await orderService.GetProductByIdAsync(productId);

            if (product != null)
            {
                await orderService.AddToCartAsync(product, purchaseType, GetUserId());
            }

            return RedirectToAction("Index", "Order"); // Redirect to the cart page
        }

        // Action to add a subscription box to the cart
        //[HttpPost]
        //public IActionResult AddToCartSubscription(int subscriptionBoxId, string purchaseType)
        //{
        //    var subscriptionBox = subscriptionBoxService.GetByIdAsync(subscriptionBoxId);

        //    if (subscriptionBox != null)
        //    {
        //        orderService.AddToCart(subscriptionBox, purchaseType);
        //    }

        //    return RedirectToAction("Index", "Order"); // Redirect to the cart page
        //}


        private string GetUserId()
            => User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? String.Empty;

    }
}
