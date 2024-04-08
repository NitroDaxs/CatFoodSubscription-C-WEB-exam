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
using static CatFoodSubscription.Common.ValidationConstants.ProductConstants;

namespace CatFoodSubscription.Services.Data
{
    public class AdminService : IAdminService
    {
        private readonly CatFoodSubscriptionDbContext context;

        public AdminService(CatFoodSubscriptionDbContext _context)
        {
            context = _context;
        }

        /// <summary>
        /// This service is responsible for fetching all of the products in the store.
        /// </summary>
        /// <returns>A collection of all the products.</returns>
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


        /// <summary>
        /// This service is responsible for fetching all of the orders made from the store.
        /// </summary>
        /// <returns>A collection of all the orders.</returns>
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


        /// <summary>
        /// This service is responsible for the search functionality in the "All Products" tab.
        /// </summary>
        /// <param name="query">This is the query by which the products will be fetched.</param>
        /// <returns>A collection of the desired products from the search query.</returns>
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

        /// <summary>
        /// This service is responsible fetching a product by given Id.
        /// </summary>
        /// <param name="id">The Id of the product.</param>
        /// <returns>A single product which meets the Id requirement.</returns>
        public async Task<AdminProductEditViewModel> GetAdminProductByIdAsync(int id)
        {
            var product = await context.Products
                .Where(p => p.Id == id)
                .AsNoTracking()
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

        /// <summary>
        /// This service is responsible for fetching the product which will be soft deleted.
        /// </summary>
        /// <param name="id">The Id of the product.</param>
        /// <returns>A single product which meets the Id requirement.</returns>
        public async Task<AdminDeleteViewModel> GetAdminDeleteProductByIdAsync(int id)
        {
            var productToDelete = await context.Products
                .Where(p => p.Id == id)
                .AsNoTracking()
                .Select(p => new AdminDeleteViewModel()
                {
                    Id = id,
                    Name = p.Name,
                })
                .FirstOrDefaultAsync();

            if (productToDelete == null)
            {
                return null;
            }

            return productToDelete;
        }

        /// <summary>
        /// This service is responsible for the "Edit" functionality of the product.
        /// </summary>
        /// <param name="model">The model in the "Edit" form.</param>
        public async Task EditAdminProductByIdAsync(AdminProductEditViewModel model)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == model.Id);

            if (product == null)
            {
                throw new InvalidOperationException();
            }

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// This service is responsible for the soft "Delete" functionality for the product.
        /// </summary>
        /// <param name="model">The model from which we fetch the correct product to soft delete.</param>
        public async Task ConfirmAdminDeleteProductAsync(AdminDeleteViewModel model)
        {
            var productToDelete = await context.Products.FirstOrDefaultAsync(p => p.Id == model.Id);

            if (productToDelete == null)
            {
                throw new InvalidOperationException();
            }

            productToDelete.IsDeleted = true;

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// This service is responsible for fetching the product that will be restored.
        /// </summary>
        /// <param name="id">The Id of the product to be restored.</param>
        /// <returns>A single product which meets the Id requirement for restoration.</returns>
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

            if (productToDelete == null)
            {
                return null;
            }

            return productToDelete;
        }

        /// <summary>
        /// This service is responsible for confirming the restoration of a previously soft-deleted product.
        /// </summary>
        /// <param name="model">The model containing the Id of the product to restore.</param>
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

        /// <summary>
        /// This service is responsible for fetching orders by a specific Id.
        /// </summary>
        /// <param name="id">The Id of the order.</param>
        /// <returns>A collection of orders matching the given Id.</returns>
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

        /// <summary>
        /// This service is responsible for fetching an order by Id to change its status.
        /// </summary>
        /// <param name="id">The Id of the order whose status is to be changed.</param>
        /// <returns>A view model for changing the status of an order.</returns>
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

            if (order == null)
            {
                return null;
            }

            return order;
        }

        /// <summary>
        /// This service is responsible for updating the status of an order.
        /// </summary>
        /// <param name="model">The model containing the new status information for the order.</param>
        public async Task UpdateAdminOrderStatus(AdminOrderChangeStatusViewModel model)
        {
            var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == model.Id);

            if (order == null)
            {
                throw new InvalidOperationException();
            }

            var status = await context.Statuses.FirstOrDefaultAsync(s => s.Id == model.StatusId);

            if (status == null)
            {
                throw new InvalidOperationException();
            }

            order.Status = status;

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// This service is responsible for fetching all available order statuses except for the default one.
        /// </summary>
        /// <returns>A collection of all available order statuses.</returns>
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

        /// <summary>
        /// This service is responsible for fetching all product categories.
        /// </summary>
        /// <returns>A collection of all product categories.</returns>
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

        /// <summary>
        /// This service is responsible for providing a detailed summary of an order by its Id.
        /// </summary>
        /// <param name="id">The Id of the order.</param>
        /// <returns>A detailed summary of the specified order.</returns>
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

        /// <summary>
        /// This service is responsible for adding a new product to the store.
        /// </summary>
        /// <param name="model">The model containing the new product information.</param>
        public async Task AddNewProductAsync(AdminAddProductViewModel model)
        {
            Product newProduct = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                ImageUrl = model.ImageUrl ?? ProductDefaultImage,
                IsSubscription = model.IsSubscription,
                CategoryId = model.CategoryId ?? 0
            };

            await context.Products.AddAsync(newProduct);
            await context.SaveChangesAsync();
        }
    }
}
