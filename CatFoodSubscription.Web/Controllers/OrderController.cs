using CatFoodSubscription.Services.Data.Interfaces;
using CatFoodSubscription.Web.Infrastructure.Extensions;
using CatFoodSubscription.Web.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;
using static CatFoodSubscription.Common.ValidationConstants;

namespace CatFoodSubscription.Web.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly ISubscriptionBoxService subscriptionBoxService;
        private readonly ILogger<OrderController> logger;

        public OrderController(IOrderService _orderService, ISubscriptionBoxService _subscriptionBoxService, ILogger<OrderController> _logger)
        {
            orderService = _orderService;
            subscriptionBoxService = _subscriptionBoxService;
            logger = _logger;
        }

        /// <summary>
        /// This method returns the product cart.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var summary = await orderService.GetOrderSummaryAsync(User.GetId());
                return View(summary);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = errorNotification;
                logger.LogError($"Error has occurred while trying to fetch a order summary {0}", DateTime.Now);
                return RedirectToAction("Index", "SubscriptionBox");
            }
        }

        /// <summary>
        /// This method is responsible for the increasing or decreasing product quantity in the cart.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int productId, string action)
        {
            try
            {
                await orderService.UpdateProductQuantityAsync(productId, action, User.GetId());
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = errorNotification;
                logger.LogError($"Error has occurred while trying to fetch a order summary {0}", DateTime.Now);
                return RedirectToAction("Index");
            }

        }

        /// <summary>
        /// This method retuns the CheckOut form for the current order.
        /// </summary>
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

        /// <summary>
        /// This method is responsible for taking the CheckOut information and processing the order.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CheckOut(OrderCheckOutFormViewModel model)
        {
            var userId = User.GetId();

            if (!ModelState.IsValid)
            {
                var summary = await orderService.GetCheckOutSummaryAsync(userId);
                return View("CheckOut", summary);
            }

            try
            {
                await orderService.ProcessOrderAsync(model, userId);
                return RedirectToAction("Index", "SubscriptionBox");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = errorNotification;
                logger.LogError($"Error has occurred while trying to fetch a order summary {0}!", DateTime.Now);
                return RedirectToAction("Index", "Order");
            }

        }

        /// <summary>
        /// This method is responsible for adding products to the cart.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            try
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
            catch (Exception)
            {
                TempData["ErrorMessage"] = errorNotification;
                logger.LogError($"Error has occurred while trying to add a product to cart {0}!", DateTime.Now);
                return RedirectToAction("Index", "SubscriptionBox");
            }
        }

        /// <summary>
        /// This method is responsible for adding subscription box to the cart.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddToCartSubscription(int subscriptionBoxId)
        {
            try
            {
                var subscriptionBox = await subscriptionBoxService.GetByIdAsync(subscriptionBoxId);

                if (subscriptionBox != null)
                {
                    await orderService.AddSubscriptionBoxToCartAsync(subscriptionBox, User.GetId());
                }

                return RedirectToAction("Index", "Order"); // Redirect to the cart page
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = errorNotification;
                logger.LogError($"Error has occurred while trying to add a subscription box to cart! {DateTime.Now}");
                return RedirectToAction("Index", "SubscriptionBox");
            }
        }


        /// <summary>
        /// This method is responsible for removing the subscription box from the cart.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> RemoveSubscriptionBox(int orderId)
        {
            try
            {
                await orderService.RemoveSubscriptionBoxAsync(orderId);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = errorNotification;
                logger.LogError($"Error has occurred while trying to remove a subscription box to cart! {DateTime.Now}");
                return RedirectToAction("Index", "SubscriptionBox");
            }
        }


        /// <summary>
        /// This method fetches all of the customer's orders.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Orders()
        {
            try
            {
                var orders = await orderService.GetAllOrdersByIdAsync(User.GetId());
                return View(orders);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = errorNotification;
                logger.LogError($"Error has occurred while trying to fetch all of the customer's orders! {DateTime.Now}");
                return RedirectToAction("Index", "SubscriptionBox");
            }

        }

        /// <summary>
        /// This method fetches the wanted order.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Summary(int id)
        {
            try
            {
                var order = await orderService.OrderSummaryAsync(id);

                return View(order);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = errorNotification;
                logger.LogError($"Error has occurred while trying to fetch one of the customer's orders! {DateTime.Now}");
                return RedirectToAction("Orders", "Order");
            }
        }
    }
}
