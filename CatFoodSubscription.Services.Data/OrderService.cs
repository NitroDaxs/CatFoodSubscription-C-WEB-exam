using CatFoodSubscription.Data;
using CatFoodSubscription.Data.Models;
using CatFoodSubscription.Services.Data.Interfaces;
using CatFoodSubscription.Web.ViewModels.Order;
using CatFoodSubscription.Web.ViewModels.SubscriptionBox;
using Microsoft.EntityFrameworkCore;
using static CatFoodSubscription.Common.ValidationConstants;

namespace CatFoodSubscription.Services.Data
{
    public class OrderService : IOrderService
    {
        private readonly CatFoodSubscriptionDbContext context;

        public OrderService(CatFoodSubscriptionDbContext _context)
        {
            context = _context;
        }

        /// <summary>
        /// This service is responsible for returning the cart of the current user.
        /// </summary>
        /// <param name="id">This is the current user's Id.</param>
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

            if (orderSummary == null)
            {
                throw new InvalidOperationException();
            }
            return orderSummary;
        }

        /// <summary>
        /// This service is responsible for fetching the desired product
        /// </summary>
        /// <param name="id">This is the Id of the desired Product</param>
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

            if (product == null)
            {
                throw new InvalidOperationException();
            }

            return product;
        }

        /// <summary>
        /// This service is responsible for fetching the checkout items.
        /// </summary>
        /// <param name="id">This is the Id of the current user</param>
        public async Task<OrderCheckOutFormViewModel> GetCheckOutSummaryAsync(string id)
        {
            var order = await context.Orders
                .Where(o => o.Customer.Id == id && o.Status.Id == 1)
                .Include(o => o.SubscriptionBox)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (order == null)
            {
                throw new InvalidOperationException();
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

            if (orderSummary == null)
            {
                throw new InvalidOperationException();
            }

            return orderSummary;
        }

        /// <summary>
        /// This service is responsible for processing the current order.
        /// </summary>
        /// <param name="model">This is the form for the address.</param>
        /// <param name="id">This is the Id of the current user</param>
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

        /// <summary>
        /// This service is responsible for adding the desired product to the cart.
        /// </summary>
        /// <param name="product">Model of the product.</param>
        /// <param name="id">This is the Id of the current user</param>
        public async Task AddToCartAsync(OrderProductViewModel product, string id)
        {
            if (product == null)
            {
                throw new InvalidOperationException();
            }

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

        /// <summary>
        /// This is a private method responsible for adding a new order to the Db
        /// </summary>
        /// <param name="id">This is the current user's Id</param>
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

        /// <summary>
        /// This service is responsible for increasing or decreasing the product quantity in the cart.
        /// </summary>
        /// <param name="productId">This is the product Id.</param>
        /// <param name="action">This is the action "increase" or "decrease"</param>
        /// <param name="id">This is the current user's Id</param>
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

        /// <summary>
        /// This service is responsible for adding a subscription box to the cart
        /// </summary>
        /// <param name="subscriptionBox">Model of the subscription box.</param>
        /// <param name="id">Id of the current User</param>
        public async Task AddSubscriptionBoxToCartAsync(SubscriptionBoxAllViewModel subscriptionBox, string id)
        {
            var order = await context.Orders
                .Where(o => o.CustomerId == id && o.StatusId == 1)
                .Include(o => o.SubscriptionBox)
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
            }

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// This service is responsible for removing the subscription box from the cart.
        /// </summary>
        /// <param name="orderId">This is the order's Id.</param>
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

        /// <summary>
        /// This service fetches all of the order done by the User.
        /// </summary>
        /// <param name="id">This is the User's Id</param>
        /// <returns></returns>
        public async Task<IEnumerable<OrderAllViewModel>> GetAllOrdersByIdAsync(string id)
        {
            var orders = await context.Orders
                .Where(o => o.CustomerId == id && o.StatusId != 1)
                .Select(o => new OrderAllViewModel()
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate.ToString(DateTimeFormat),
                    TotalPrice = o.ProductsOrders.Sum(p => p.Product.Price),
                    FirstName = o.Address.FirstName,
                    LastName = o.Address.LastName,
                    Status = o.Status.Name
                })
                .AsNoTracking()
                .ToListAsync();

            return orders;
        }

        /// <summary>
        /// This service is responsible for getting the summary of the desired order to the User.
        /// </summary>
        /// <param name="id">This is the order Id.</param>
        public async Task<OrderSummaryViewModel> OrderSummaryAsync(int id)
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
                Address = new OrderAddressViewModel()
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
    }
}