using CatFoodSubscription.Services.Data.Interfaces;
using CatFoodSubscription.Web.ViewModels.Admin.Order;
using Microsoft.AspNetCore.Mvc;
using static CatFoodSubscription.Common.ValidationConstants;

namespace CatFoodSubscription.Web.Areas.Admin.Controllers
{
    public class OrderController : BaseAdminController
    {
        private readonly IAdminService adminService;
        private readonly ILogger<OrderController> logger;

        public OrderController(IAdminService _adminService, ILogger<OrderController> logger)
        {
            adminService = _adminService;
            this.logger = logger;
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

            try
            {
                await adminService.UpdateAdminOrderStatus(model);

                return RedirectToAction("AllOrders", "Home");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = errorNotification;
                logger.LogError($"Error has occurred while trying to save order status {0}", DateTime.Now);
                return RedirectToAction("AllOrders", "Home");
            }

        }

        [HttpGet]
        public async Task<IActionResult> Info(int id)
        {
            var order = await adminService.OrderSummaryByIdAsync(id);
            return View(order);
        }
    }
}
