using CatFoodSubscription.Services.Data.Interfaces;
using CatFoodSubscription.Web.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;

namespace CatFoodSubscription.Web.Areas.Admin.Controllers
{
    public class ProductController : BaseAdminController
    {
        private readonly IAdminService adminService;

        public ProductController(IAdminService _adminService)
        {
            adminService = _adminService;
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await adminService.GetAdminProductByIdAsync(id);

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Save(AdminProductEditViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                var product = await adminService.GetAdminProductByIdAsync(id);
                return View("Edit", product);
            }
            await adminService.EditAdminProductByIdAsync(model);

            return RedirectToAction("AllProducts", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await adminService.GetAdminDeleteProductByIdAsync(id);

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(AdminDeleteViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                var product = await adminService.GetAdminProductByIdAsync(id);
                return View("Edit", product);
            }
            await adminService.ConfirmAdminDeleteProductAsync(model);

            return RedirectToAction("AllProducts", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Cancel(int id)
        {
            return RedirectToAction("AllProducts", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Restore(int id)
        {
            var productToRestore = await adminService.GetAdminRestoreProductByIdAsync(id);

            return View(productToRestore);
        }


        [HttpPost]
        public async Task<IActionResult> ConfirmRestore(AdminRestoreViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                var product = await adminService.GetAdminProductByIdAsync(id);
                return View("Edit", product);
            }
            await adminService.ConfirmAdminRestoreProductAsync(model);

            return RedirectToAction("AllProducts", "Home");
        }
    }
}
