using CatFoodSubscription.Data;
using CatFoodSubscription.Services.Data.Interfaces;
using CatFoodSubscription.Web.ViewModels.Order;
using CatFoodSubscription.Web.ViewModels.Subscription;
using Microsoft.EntityFrameworkCore;

namespace CatFoodSubscription.Services.Data
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly CatFoodSubscriptionDbContext context;

        public SubscriptionService(CatFoodSubscriptionDbContext _context)
        {
            context = _context;
        }


        public async Task<SubscriptionAllViewModel> GetOrderSubscriptionProductAsync(string id)
        {
            var subscriptionViewModel = new SubscriptionAllViewModel
            {
                Orders = await context.Orders
                    .Include(o => o.ProductsOrders)
                    .ThenInclude(po => po.Product)
                    .Where(o => o.ProductsOrders.Any(po => po.Product.IsSubscription) && o.CustomerId == id && o.IsSubscriptionCanceled == false && o.StatusId != 1)
                    .Select(o => new SubscriptionOrderViewModel
                    {
                        OrderId = o.Id,
                        OrderDate = o.OrderDate,
                        SubscriptionBox = new OrderSubscriptionBoxViewModel
                        {
                            Id = o.SubscriptionBox != null ? o.SubscriptionBox.Id : 0,
                            Name = o.SubscriptionBox != null ? o.SubscriptionBox.Name : "",
                            Price = o.SubscriptionBox != null ? o.SubscriptionBox.Price : 0,
                            ImageUrl = o.SubscriptionBox != null ? o.SubscriptionBox.ImageUrl ?? "" : ""
                        },
                        Products = o.ProductsOrders
                            .Where(po => po.Product.IsSubscription)
                            .Select(po => new OrderProductViewModel
                            {
                                Id = po.Product.Id,
                                Name = po.Product.Name,
                                Price = po.Product.Price,
                                ImageUrl = po.Product.ImageUrl,
                                Quantity = po.Quantity,
                                IsSubscription = po.Product.IsSubscription
                            })
                            .ToList()
                    })
                    .ToListAsync()
            };

            if (subscriptionViewModel == null)
            {
                throw new NullReferenceException();
            }

            return subscriptionViewModel;
        }

        public async Task CancelSubscription(int id)
        {
            var order = await context.Orders
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                throw new NullReferenceException();
            }

            order.IsSubscriptionCanceled = true;
            await context.SaveChangesAsync();
        }
    }
}
