using CatFoodSubscription.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatFoodSubscription.Web.Areas.Admin.Controllers
{
    public class SubscriptionController : BaseAdminController
    {
        private readonly IAdminService adminService;
        private readonly ILogger<ProductController> logger;

        public SubscriptionController(IAdminService _adminService
            , ILogger<ProductController> logger)
        {
            adminService = _adminService;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Renew(int id)
        {
            await adminService.UpdateAdminSubscriptionRenewalDate(id);
            return RedirectToAction("AllSubscriptions", "Home");
        }
    }
}
