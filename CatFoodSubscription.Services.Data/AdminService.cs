using CatFoodSubscription.Data;
using CatFoodSubscription.Data.Models;
using CatFoodSubscription.Services.Data.Interfaces;
using CatFoodSubscription.Web.ViewModels.Admin.Address;
using CatFoodSubscription.Web.ViewModels.Admin.Category;
using CatFoodSubscription.Web.ViewModels.Admin.Order;
using CatFoodSubscription.Web.ViewModels.Admin.Product;
using CatFoodSubscription.Web.ViewModels.Admin.Status;
using CatFoodSubscription.Web.ViewModels.Admin.SubsctiptionBox;
using Microsoft.EntityFrameworkCore;

namespace CatFoodSubscription.Services.Data
{
    public class AdminService : IAdminService
    {
        private readonly CatFoodSubscriptionDbContext context;

        public AdminService(CatFoodSubscriptionDbContext _context)
        {
            context = _context;
        }
        public async Task<IEnumerable<AdminAllProductsViewModel>> GetAdminProductAllAsync()
        {
            var products = await context.Products
                .AsNoTracking()
                .Select(p => new AdminAllProductsViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    IsDeleted = p.IsDeleted
                })
                .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<AdminAllOrdersViewModel>> GetAdminOrderAllAsync()
        {
            var orders = await context.Orders
                .Where(o => o.StatusId != 1)
                .AsNoTracking()
                .Select(o => new AdminAllOrdersViewModel()
                {
                    Id = o.Id,
                    FirstName = o.Address.FirstName,
                    LastName = o.Address.LastName,
                    Status = o.Status.Name
                })
                .ToListAsync();

            return orders;
        }

        public async Task<IEnumerable<AdminAllProductsViewModel>> GetAdminProductBySearchAsync(string query)
        {
            var products = await context.Products
                .AsNoTracking()
                .Where(p => p.Name.Contains(query))
                .Select(p => new AdminAllProductsViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                })
                .ToListAsync();

            return products;
        }

        public async Task<AdminProductEditViewModel> GetAdminProductByIdAsync(int id)
        {
            var product = await context.Products
                .Where(p => p.Id == id)
                .Select(p => new AdminProductEditViewModel()
                {
                    Id = id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price
                })
                .FirstOrDefaultAsync();

            if (product == null)
            {
                throw new Exception();
            }

            return product;
        }

        public async Task<AdminDeleteViewModel> GetAdminDeleteProductByIdAsync(int id)
        {
            var productToDelete = await context.Products
                .Where(p => p.Id == id)
                .Select(p => new AdminDeleteViewModel()
                {
                    Id = id,
                    Name = p.Name,
                })
                .FirstOrDefaultAsync();

            return productToDelete;
        }



        public async Task EditAdminProductByIdAsync(AdminProductEditViewModel model)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == model.Id);

            if (product == null)
            {
                throw new Exception();
            }

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;

            await context.SaveChangesAsync();
        }

        public async Task ConfirmAdminDeleteProductAsync(AdminDeleteViewModel model)
        {
            var productToDelete = await context.Products.FirstOrDefaultAsync(p => p.Id == model.Id);

            if (productToDelete == null)
            {
                throw new Exception();
            }

            productToDelete.IsDeleted = true;

            await context.SaveChangesAsync();
        }
        public async Task<AdminRestoreViewModel> GetAdminRestoreProductByIdAsync(int id)
        {
            var productToDelete = await context.Products
                .Where(p => p.Id == id)
                .Select(p => new AdminRestoreViewModel()
                {
                    Id = id,
                    Name = p.Name,
                })
                .FirstOrDefaultAsync();

            return productToDelete;
        }
        public async Task ConfirmAdminRestoreProductAsync(AdminRestoreViewModel model)
        {
            var productToDelete = await context.Products.FirstOrDefaultAsync(p => p.Id == model.Id);

            if (productToDelete == null)
            {
                throw new Exception();
            }

            productToDelete.IsDeleted = false;

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AdminAllOrdersViewModel>> GetAdminOrderByIdAsync(int id)
        {
            var order = await context.Orders
                .Where(o => o.Id == id && o.StatusId != 1)
                .AsNoTracking()
                .Select(o => new AdminAllOrdersViewModel()
                {
                    Id = id,
                    FirstName = o.Address.FirstName,
                    LastName = o.Address.LastName,
                    Status = o.Status.Name
                })
                .ToListAsync();

            return order;
        }

        public async Task<AdminOrderChangeStatusViewModel> GetAdminOrderByIdChangeStatusAsync(int id)
        {
            var order = await context.Orders
                .Where(o => o.Id == id && o.StatusId != 1)
                .AsNoTracking()
                .Select(o => new AdminOrderChangeStatusViewModel()
                {
                    Id = o.Id,
                    CustomerId = o.CustomerId,
                })
                .FirstOrDefaultAsync();

            return order;
        }

        public async Task UpdateAdminOrderStatus(AdminOrderChangeStatusViewModel model)
        {
            var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == model.Id);

            if (order == null)
            {
                throw new Exception();
            }

            var status = await context.Statuses.FirstOrDefaultAsync(s => s.Id == model.StatusId);

            if (status == null)
            {
                throw new Exception();
            }

            order.Status = status;

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AdminStatusViewModel>> GetAdminOrderStatusesAsync()
        {
            var statuses = await context.Statuses
                .Where(s => s.Id != 1)
                .Select(s => new AdminStatusViewModel()
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToListAsync();

            return statuses;
        }

        public async Task<IEnumerable<AdminCategoryViewModel>> GetAdminProductCategoriesAsync()
        {
            var categories = await context.Categories
                .Select(c => new AdminCategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();

            return categories;
        }

        public async Task<AdminOrderSummaryViewModel> OrderSummaryByIdAsync(int id)
        {
            Order? order = await context.Orders
                .Where(o => o.Id == id && o.StatusId != 1)
                .Include(o => o.SubscriptionBox)
                .Include(o => o.ProductsOrders)
                .ThenInclude(po => po.Product)
                .Include(o => o.Address)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (order == null)
            {
                throw new NullReferenceException();
            }

            var orderSummary = new AdminOrderSummaryViewModel()
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                SubscriptionBox = new AdminSubscriptionBoxViewModel()
                {
                    Id = order.SubscriptionBox?.Id ?? 0,
                    Name = order.SubscriptionBox?.Name ?? "",
                    Price = order.SubscriptionBox?.Price ?? 0,
                    ImageUrl = order.SubscriptionBox?.ImageUrl ?? ""
                },
                Address = new AdminAddressViewModel()
                {
                    Country = order.Address.Country,
                    City = order.Address.City,
                    Street = order.Address.Street,
                    Email = order.Address.Email,
                    PhoneNumber = order.Address.PhoneNumber,
                    FirstName = order.Address.FirstName,
                    LastName = order.Address.LastName,
                    PostalCode = order.Address.PostalCode,
                },
                Products = order.ProductsOrders.Select(po => new AdminAllProductsViewModel()
                {
                    Id = po.Product.Id,
                    Name = po.Product.Name,
                    Price = po.Product.Price,
                    ImageUrl = po.Product.ImageUrl,
                    Quantity = po.Quantity,
                    IsSubscription = po.Product.IsSubscription
                }).ToList()
            };

            return orderSummary;
        }
    }
}
