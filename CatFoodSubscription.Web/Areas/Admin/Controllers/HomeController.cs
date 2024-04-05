﻿using CatFoodSubscription.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatFoodSubscription.Web.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        private readonly IAdminService adminService;

        public HomeController(IAdminService _adminService)
        {
            adminService = _adminService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AllProducts()
        {
            var products = await adminService.GetAdminProductAllAsync();
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> SearchedProducts(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                var products = await adminService.GetAdminProductBySearchAsync(query);
                return View("AllProducts", products);
            }

            return RedirectToAction("AllProducts");
        }

        [HttpPost]
        public async Task<IActionResult> SearchedOrders(int id)
        {
            if (id != 0)
            {
                var order = await adminService.GetAdminOrderByIdAsync(id);

                return View("AllOrders", order);
            }

            return RedirectToAction("AllOrders");
        }


        [HttpGet]
        public async Task<IActionResult> AllOrders()
        {
            var orders = await adminService.GetAdminOrderAllAsync();
            return View(orders);
        }
    }
}
