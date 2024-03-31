using Microsoft.AspNetCore.Mvc;

namespace CatFoodSubscription.Web.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ChangeStatus(int id)
        {
            return View();
        }
    }
}
