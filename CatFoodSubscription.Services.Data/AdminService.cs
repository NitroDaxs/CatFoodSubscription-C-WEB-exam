using CatFoodSubscription.Data;
using CatFoodSubscription.Services.Data.Interfaces;
using CatFoodSubscription.Web.Areas.Admin.ViewModels;
using CatFoodSubscription.Web.ViewModels.Admin;
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
    }
}
