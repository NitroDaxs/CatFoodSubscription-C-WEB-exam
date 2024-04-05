using CatFoodSubscription.Services.Data.Interfaces;
using CatFoodSubscription.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CatFoodSubscription.Web.Controllers
{
    public class SubscriptionController : BaseController
    {
        private readonly ISubscriptionService subscriptionService;

        public SubscriptionController(ISubscriptionService _subscriptionService)
        {
            subscriptionService = _subscriptionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var subscriptionViewModel = await subscriptionService.GetOrderSubscriptionProductAsync(User.GetId());

                return View(subscriptionViewModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "SubscriptionBox");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CancelSubscription(int id)
        {
            try
            {
                await subscriptionService.CancelSubscription(id);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "SubscriptionBox");
            }
        }
    }
}
