using Microsoft.AspNetCore.Mvc;

namespace CatFoodSubscription.Web.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
