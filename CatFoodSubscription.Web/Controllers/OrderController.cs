using CatFoodSubscription.Services.Data.Interfaces;
using CatFoodSubscription.Web.Infrastructure.Extensions;
using CatFoodSubscription.Web.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;

namespace CatFoodSubscription.Web.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly ISubscriptionBoxService subscriptionBoxService;


        public OrderController(IOrderService _orderService, ISubscriptionBoxService _subscriptionBoxService)
        {
            orderService = _orderService;
            subscriptionBoxService = _subscriptionBoxService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var summary = await orderService.GetOrderSummaryAsync(User.GetId());

            return View(summary);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int productId, string action)
        {
            await orderService.UpdateProductQuantityAsync(productId, action, User.GetId());

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> CheckOut()
        {
            var summary = await orderService.GetCheckOutSummaryAsync(User.GetId());

            if (!summary.Products.Any() && summary.SubscriptionBox.Id == 0)
            {
                return RedirectToAction("Index", "Product");
            }

            return View(summary);
        }

        [HttpPost]
        public async Task<IActionResult> CheckOut(OrderCheckOutFormViewModel model)
        {
            var userId = User.GetId();

            if (!ModelState.IsValid)
            {
                var summary = await orderService.GetCheckOutSummaryAsync(userId);
                return View("CheckOut", summary);
            }

            await orderService.ProcessOrderAsync(model, userId);
            return RedirectToAction("Index", "SubscriptionBox");
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, bool purchaseType)
        {
            var product = await orderService.GetProductByIdAsync(productId);

            if (product != null)
            {
                await orderService.AddToCartAsync(product, User.GetId());

                TempData["SuccessMessage"] = $"Product added to the cart successfully!";
                TempData["ProductImageUrl"] = product.ImageUrl;
            }

            return RedirectToAction("Index", "Product");
        }

        //Action to add a subscription box to the cart
        [HttpPost]
        public async Task<IActionResult> AddToCartSubscription(int subscriptionBoxId)
        {
            var subscriptionBox = await subscriptionBoxService.GetByIdAsync(subscriptionBoxId);

            if (subscriptionBox != null)
            {
                await orderService.AddSubscriptionBoxToCartAsync(subscriptionBox, User.GetId());
            }

            return RedirectToAction("Index", "Order"); // Redirect to the cart page
        }


        [HttpPost]
        public async Task<IActionResult> RemoveSubscriptionBox(int orderId)
        {
            await orderService.RemoveSubscriptionBoxAsync(orderId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Orders()
        {
            var orders = await orderService.GetAllOrdersByIdAsync(User.GetId());
            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> Summary(int id)
        {
            var order = await orderService.OrderSummaryAsync(id);

            return View(order);
        }
    }
}
