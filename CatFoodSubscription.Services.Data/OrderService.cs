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

        public async Task<OrderSummaryViewModel> GetOrderSummaryAsync(string id)
        {
            Order? order = await context.Orders
                .Where(o => o.Customer.Id == id && o.StatusId == 1)
                .Include(o => o.SubscriptionBox)
                .Include(o => o.ProductsOrders)
                .ThenInclude(po => po.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (order == null)
            {
                order = await this.AddOrderAsync(id);
            }


            if (order == null)
            {
                throw new NullReferenceException();
            }

            var orderSummary = new OrderSummaryViewModel()
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                SubscriptionBox = new OrderSubscriptionBoxViewModel
                {
                    Id = order.SubscriptionBox?.Id ?? 0,
                    Name = order.SubscriptionBox?.Name ?? "",
                    Price = order.SubscriptionBox?.Price ?? 0,
                    ImageUrl = order.SubscriptionBox?.ImageUrl ?? ""
                },
                Products = order.ProductsOrders.Select(po => new OrderProductViewModel
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

        public async Task<OrderCheckOutFormViewModel> GetCheckOutSummaryAsync(string id)
        {
            var order = await context.Orders
                .Where(o => o.Customer.Id == id && o.Status.Id == 1)
                .Include(o => o.SubscriptionBox)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (order == null)
            {
                return null; // or throw an exception, or return a default OrderCheckOutFormViewModel
            }


            var products = await context.ProductsOrders
                .Where(po => po.OrderId == order.Id)
                .Include(po => po.Product)
                .AsNoTracking()
                .ToListAsync();

            var orderSummary = new OrderCheckOutFormViewModel()
            {
                OrderId = order.Id,
                CustomerId = id,
                SubscriptionBox = new OrderSubscriptionBoxViewModel
                {
                    Id = order.SubscriptionBox?.Id ?? 0,
                    Name = order.SubscriptionBox?.Name ?? "",
                    Price = order.SubscriptionBox?.Price ?? 0,
                    ImageUrl = order.SubscriptionBox?.ImageUrl ?? ""
                },
                Address = new OrderAddressViewModel(),
                Products = products.Select(po => new OrderProductViewModel
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


        public async Task ProcessOrderAsync(OrderCheckOutFormViewModel model, string id)
        {
            var address = new Address()
            {
                Country = model.Address.Country,
                City = model.Address.City,
                Street = model.Address.Street,
                PostalCode = model.Address.PostalCode,
                PhoneNumber = model.Address.PhoneNumber,
                FirstName = model.Address.FirstName,
                LastName = model.Address.LastName,
                Email = model.Address.Email
            };

            context.Addresses.Add(address);
            await context.SaveChangesAsync();

            var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == model.OrderId);

            if (order != null)
            {
                order.StatusId = 2;
                order.OrderDate = DateTime.Now;
                order.AddressId = address.Id;

                await context.SaveChangesAsync();
            }

            await this.AddOrderAsync(id);
        }

        public async Task AddToCartAsync(OrderProductViewModel product, string id)
        {
            // Find the order associated with the customer
            var order = await context.Orders
                .Where(o => o.CustomerId == id && o.StatusId == 1)
                .Include(o => o.ProductsOrders)
                .ThenInclude(po => po.Product)
                .FirstOrDefaultAsync();

            // If the order doesn't exist, create a new order
            if (order == null)
            {
                order = await AddOrderAsync(id);
            }

            // Check if the product is already in the order
            var existingProductOrder = order.ProductsOrders.FirstOrDefault(po => po.ProductId == product.Id);

            if (existingProductOrder != null)
            {
                // If the product is already in the order, increment the quantity
                existingProductOrder.Quantity++;
            }
            else
            {
                // If the product is not in the order, create a new ProductOrder entry
                var productOrder = new ProductOrder
                {
                    ProductId = product.Id,
                    OrderId = order.Id,
                    Quantity = 1 // Assuming initial quantity is 1
                };

                context.ProductsOrders.Add(productOrder);
            }

            // Save changes to the database
            await context.SaveChangesAsync();
        }

        private async Task<Order> AddOrderAsync(string id)
        {
            Order order;
            order = new Order
            {
                CustomerId = id,
                StatusId = 1,
                ProductsOrders = new List<ProductOrder>()
            };

            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
            return order;
        }


        public async Task UpdateProductQuantityAsync(int productId, string action, string id)
        {
            var order = await context.Orders
                .Where(o => o.CustomerId == id && o.StatusId == 1)
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
                .Where(o => o.CustomerId == id && o.StatusId == 1)
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





