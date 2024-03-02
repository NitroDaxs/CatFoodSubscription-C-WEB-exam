using CatFoodSubscription.Data;
using CatFoodSubscription.Data.Models;
using CatFoodSubscription.Services.Data.Interfaces;
using CatFoodSubscription.Web.ViewModels.Order;
using CatFoodSubscription.Web.ViewModels.SubscriptionBox;
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
                .Include(o => o.SubscriptionBox)
                .Include(o => o.ProductsOrders)
                .ThenInclude(po => po.Product)
                .AsNoTracking()
                .ToListAsync();

            var orderSummary = orders.Select(o => new OrderSummaryViewModel
            {
                Id = o.Id,
                CustomerId = id,
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

        public async Task AddToCartAsync(OrderProductViewModel product, string id)
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
                var productOrder = new ProductOrder
                {
                    ProductId = product.Id,
                    OrderId = order.Id,
                };

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

        public async Task AddSubscriptionBoxToCartAsync(SubscriptionBoxAllViewModel subscriptionBox, string id)
        {
            var order = await context.Orders
                .Where(o => o.CustomerId == id)
                .Include(o => o.SubscriptionBox) // Make sure to include the SubscriptionBox
                .FirstOrDefaultAsync();

            if (order == null)
            {
                order = new Order
                {
                    CustomerId = id,
                    StatusId = 1,
                    SubscriptionBox = new SubscriptionBox
                    {
                        Id = subscriptionBox.Id,
                        Name = subscriptionBox.Name,
                        ImageUrl = subscriptionBox.ImageUrl,
                        Price = subscriptionBox.Price,
                        Description = subscriptionBox.Description,
                    }
                };

                context.Orders.Add(order);
            }
            else
            {
                if (order.SubscriptionBox == null)
                {
                    order.SubscriptionBox = new SubscriptionBox
                    {
                        Id = subscriptionBox.Id,
                        Name = subscriptionBox.Name,
                        ImageUrl = subscriptionBox.ImageUrl,
                        Price = subscriptionBox.Price,
                        Description = subscriptionBox.Description,
                    };
                }
                else
                {
                    // Handle the case where a subscription box is already in the order (optional)
                    // You may want to update the existing subscription box or handle it as needed
                }
            }

            await context.SaveChangesAsync();
        }

        public async Task RemoveSubscriptionBoxAsync(int orderId)
        {
            var order = await context.Orders
                .Where(o => o.Id == orderId)
                .Include(o => o.SubscriptionBox)
                .FirstOrDefaultAsync();

            if (order != null)
            {
                order.SubscriptionBoxId = null;

                await context.SaveChangesAsync();
            }
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



