using CatFoodSubscription.Services.Data.Interfaces;
using CatFoodSubscription.Web.ViewModels.Admin.Order;
using Microsoft.AspNetCore.Mvc;

namespace CatFoodSubscription.Web.Areas.Admin.Controllers
{
    public class OrderController : BaseAdminController
    {
        private readonly IAdminService adminService;

        public OrderController(IAdminService _adminService)
        {
            adminService = _adminService;
        }
        [HttpGet]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            var orderToChange = await adminService.GetAdminOrderByIdChangeStatusAsync(id);
            orderToChange.Status = await adminService.GetAdminOrderStatusesAsync();
            return View("ChangeStatus", orderToChange);
        }

        [HttpPost]
        public async Task<IActionResult> SaveStatus(AdminOrderChangeStatusViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                model.Status = await adminService.GetAdminOrderStatusesAsync();
                return View("ChangeStatus", model);
            }

            await adminService.UpdateAdminOrderStatus(model);

            return RedirectToAction("AllOrders", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Info(int id)
        {
            var order = await adminService.OrderSummaryByIdAsync(id);
            return View(order);
        }
    }
}
