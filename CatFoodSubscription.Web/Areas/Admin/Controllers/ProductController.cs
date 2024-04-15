using CatFoodSubscription.Services.Data.Interfaces;
using CatFoodSubscription.Web.ViewModels.Admin.Product;
using Microsoft.AspNetCore.Mvc;
using static CatFoodSubscription.Common.ValidationConstants;

namespace CatFoodSubscription.Web.Areas.Admin.Controllers
{
    public class ProductController : BaseAdminController
    {
        private readonly IAdminService adminService;
        private readonly ILogger<ProductController> logger;

        public ProductController(IAdminService _adminService
        , ILogger<ProductController> logger)
        {
            adminService = _adminService;
            this.logger = logger;
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
            try
            {
                await adminService.EditAdminProductByIdAsync(model);

                return RedirectToAction("AllProducts", "Home");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = errorNotification;
                logger.LogError($"Error has occurred while trying to fetch a product by Id {0}", DateTime.Now);
                return RedirectToAction("AllProducts", "Home");
            }
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

            try
            {
                await adminService.ConfirmAdminDeleteProductAsync(model);

                return RedirectToAction("AllProducts", "Home");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = errorNotification;
                logger.LogError($"Error has occurred while trying to confirm a product deletion {0}", DateTime.Now);
                return RedirectToAction("AllProducts", "Home");
            }

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

            try
            {
                await adminService.ConfirmAdminRestoreProductAsync(model);

                return RedirectToAction("AllProducts", "Home");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = errorNotification;
                logger.LogError($"Error has occurred while trying restore a product! {0}", DateTime.Now);
                return RedirectToAction("AllProducts", "Home");
            }

        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AdminAddProductViewModel viewModel = new AdminAddProductViewModel();
            viewModel.Status = await adminService.GetAdminProductCategoriesAsync();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AdminAddProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Status = await adminService.GetAdminProductCategoriesAsync();
                return View("Add", model);
            }
            try
            {
                await adminService.AddNewProductAsync(model);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = errorNotification;
                logger.LogError($"Error has occurred while trying to add a new product! {0}", DateTime.Now);
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
