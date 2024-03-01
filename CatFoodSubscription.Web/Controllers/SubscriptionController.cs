using Microsoft.AspNetCore.Mvc;

namespace CatFoodSubscription.Web.Controllers
{
    public class SubscriptionController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
