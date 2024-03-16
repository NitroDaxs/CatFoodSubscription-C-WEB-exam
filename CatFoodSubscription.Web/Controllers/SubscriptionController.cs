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
            var subscriptionViewModel = await subscriptionService.GetOrderSubscriptionProductAsync(User.GetId());

            return View(subscriptionViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CancelSubscription(int id)
        {
            await subscriptionService.CancelSubscription(id);

            return RedirectToAction("Index");
        }
    }
}
