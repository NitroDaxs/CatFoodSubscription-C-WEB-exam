using CatFoodSubscription.Services.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatFoodSubscription.Web.Controllers
{
    public class SubscriptionBoxController : BaseController
    {
        private readonly ISubscriptionBoxService subscriptionBoxService;

        public SubscriptionBoxController(ISubscriptionBoxService _subscriptionBoxService)
        {
            subscriptionBoxService = _subscriptionBoxService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var model = await subscriptionBoxService.GetAllAsync();

            return View(model);
        }
    }
}
