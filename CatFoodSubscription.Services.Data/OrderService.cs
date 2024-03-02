using CatFoodSubscription.Data;
using CatFoodSubscription.Data.Models;
using CatFoodSubscription.Services.Data.Interfaces;
using CatFoodSubscription.Web.ViewModels.Order;
using Microsoft.EntityFrameworkCore;

namespace CatFoodSubscription.Services.Data
{


    public class OrderService : IOrderService
    {
        private readonly CatFoodSubscriptionDbContext context;

        public OrderService(CatFoodSubscriptionDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<OrderSummaryViewModel>> GetOrderSummaryAsync(string id)
        {
            var orders = await context.Orders
                .Where(o => o.Customer.Id == id)
                .Include(o => o.Status)
                .Include(o => o.SubscriptionBox)
                .Include(o => o.ProductsOrders)
                .ThenInclude(po => po.Product)
                .ThenInclude(p => p.Category)
                .AsNoTracking()
                .ToListAsync();

            var orderSummary = orders.Select(o => new OrderSummaryViewModel
            {
                Id = o.Id,
                CustomerId = id,
                StatusId = o.StatusId,
                SubscriptionBox = new OrderSubscriptionBoxViewModel
                {
                    Id = o.SubscriptionBox?.Id ?? 0,
                    Name = o.SubscriptionBox?.Name ?? "",
                    Price = o.SubscriptionBox?.Price ?? 0,
                    ImageUrl = o.SubscriptionBox?.ImageUrl ?? ""
                },
                Products = o.ProductsOrders.Select(po => new OrderProductViewModel
                {
                    Id = po.Product.Id,
                    Name = po.Product.Name,
                    Price = po.Product.Price,
                    ImageUrl = po.Product.ImageUrl,
                    Quantity = po.Quantity
                }).ToList()
            });

            return orderSummary;
        }

        public async Task<OrderProductViewModel> GetProductByIdAsync(int id)
        {
            var product = await context.Products
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new OrderProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    Quantity = p.ProductsOrders.Sum(po => po.Quantity)
                })
                .FirstOrDefaultAsync();

            return product;
        }

        public async Task AddToCartAsync(OrderProductViewModel product, bool purchaseType, string id)
        {
            var order = await context.Orders
                .Where(o => o.CustomerId == id)
                .Include(o => o.ProductsOrders)
                .ThenInclude(po => po.Product)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                order = new Order
                {
                    CustomerId = id,
                    StatusId = 1,
                    ProductsOrders = new List<ProductOrder>()
                };

                context.Orders.Add(order);
                await context.SaveChangesAsync();
            }

            var existingProductOrder = order.ProductsOrders.FirstOrDefault(po => po.ProductId == product.Id);

            if (existingProductOrder != null)
            {
                existingProductOrder.Quantity++;
            }
            else
            {
                var productOrder = new ProductOrder();
                if (purchaseType == true)
                {
                    productOrder = new ProductOrder
                    {
                        ProductId = product.Id,
                        OrderId = order.Id,
                    };
                }
                else
                {
                    productOrder = new ProductOrder
                    {
                        ProductId = product.Id,
                        OrderId = order.Id,
                    };
                }
                context.ProductsOrders.Add(productOrder);
            }

            await context.SaveChangesAsync();
        }


        public async Task UpdateProductQuantityAsync(int productId, string action, string id)
        {
            var order = await context.Orders
                .Where(o => o.CustomerId == id && o.StatusId != 4)
                .Include(o => o.ProductsOrders)
                .ThenInclude(po => po.Product)
                .FirstOrDefaultAsync();

            ProductOrder productOrder = order.ProductsOrders
                .Where(po => po.ProductId == productId)
                .FirstOrDefault();

            if (productOrder != null)
            {
                if (action == "increase")
                {
                    productOrder.Quantity++;
                }
                else
                {
                    if (productOrder.Quantity > 1)
                    {
                        productOrder.Quantity--;
                    }
                    else
                    {
                        order.ProductsOrders.Remove(productOrder);
                    }
                }
                await context.SaveChangesAsync();
            }
        }

    }


    //public async Task AddToCartAsync(SubscriptionBox subscriptionBox, string purchaseType)
    //{

    //    var cart = await dbContext.Carts
    //        .Include(c => c.CartSubscriptionBoxes)
    //        .FirstOrDefaultAsync();

    //    if (cart == null)
    //    {
    //        cart = new Cart();
    //        dbContext.Carts.Add(cart);
    //    }

    //    var cartSubscriptionBox = new CartSubscriptionBox
    //    {
    //        SubscriptionBox = subscriptionBox,
    //        PurchaseType = purchaseType
    //    };

    //    cart.CartSubscriptionBoxes.Add(cartSubscriptionBox);

    //    await dbContext.SaveChangesAsync();
    //}
}


