using CatFoodSubscription.Data;
using CatFoodSubscription.Services.Data.Interfaces;
using CatFoodSubscription.Web.ViewModels.SubscriptionBox;
using Microsoft.EntityFrameworkCore;

namespace CatFoodSubscription.Services.Data
{
    public class SubscriptionBoxService : ISubscriptionBoxService

    {
        private readonly CatFoodSubscriptionDbContext context;

        public SubscriptionBoxService(CatFoodSubscriptionDbContext _context)
        {
            context = _context;
        }
        public async Task<IEnumerable<SubscriptionBoxAllViewModel>> GetAllAsync()
        {
            var subscriptionBoxes = await context.SubscriptionBoxes
                .Include(sb => sb.ProductSubscriptionBoxes)
                .ThenInclude(psb => psb.Product)
                .AsNoTracking()
                .ToListAsync();

            var model = subscriptionBoxes
                .Select(sb => new SubscriptionBoxAllViewModel()
                {
                    Id = sb.Id,
                    Name = sb.Name,
                    ImageUrl = sb.ImageUrl,
                    Description = sb.Description,
                    Price = sb.Price,
                    ProductSubscriptionBoxes = sb.ProductSubscriptionBoxes
                        .Select(psb => new ProductSubscriptionBoxViewModel()
                        {
                            ProductId = psb.ProductId,
                            ProductName = psb.Product.Name,
                        })
                        .ToList()
                })
                .ToList();

            return model;
        }

        public async Task<SubscriptionBoxAllViewModel> GetByIdAsync(int id)
        {
            var subscriptionBoxes = await context.SubscriptionBoxes
                .Include(sb => sb.ProductSubscriptionBoxes)
                .ThenInclude(psb => psb.Product)
                .AsNoTracking()
                .ToListAsync();

            var model = subscriptionBoxes
                .Where(sb => sb.Id == id)
                .Select(sb => new SubscriptionBoxAllViewModel()
                {
                    Id = sb.Id,
                    Name = sb.Name,
                    ImageUrl = sb.ImageUrl,
                    Description = sb.Description,
                    Price = sb.Price,
                    ProductSubscriptionBoxes = sb.ProductSubscriptionBoxes
                        .Select(psb => new ProductSubscriptionBoxViewModel()
                        {
                            ProductId = psb.ProductId,
                            ProductName = psb.Product.Name,
                        })
                        .ToList()
                })
                .FirstOrDefault();

            return model;
        }
    }
}
