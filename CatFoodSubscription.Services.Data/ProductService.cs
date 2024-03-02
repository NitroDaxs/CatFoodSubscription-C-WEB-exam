﻿using CatFoodSubscription.Data;
using CatFoodSubscription.Services.Data.Interfaces;
using CatFoodSubscription.Web.ViewModels.Product;
using Microsoft.EntityFrameworkCore;

namespace CatFoodSubscription.Services.Data
{
    public class ProductService : IProductService
    {
        private readonly CatFoodSubscriptionDbContext context;

        public ProductService(CatFoodSubscriptionDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<ProductAllViewModel>> GetProductAllAsync()
        {
            var products = await context.Products
                .AsNoTracking()
                .Select(p => new ProductAllViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    Category = p.Category.Name,
                })
                .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<ProductAllViewModel>> GetProductSearchAsync(string query)
        {

            var products = await context.Products
                .AsNoTracking()
                .Where(p => p.Name.Contains(query))
                .Select(p => new ProductAllViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    Category = p.Category.Name,
                })
                .ToListAsync();

            return products;
        }

        public async Task<ProductDetailViewModel> GetProductByIdAsync(int id)
        {
            var product = await context.Products
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new ProductDetailViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    Description = p.Description,
                    Category = p.Category.Name,
                })
                .FirstOrDefaultAsync();

            return product;
        }

        public async Task<IEnumerable<ProductSearchViewModel>> GetProductSearchBarAsync(string query)
        {
            var results = await context.Products
                .Where(p => p.Name.Contains(query))
                .Select(p => new ProductSearchViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl
                })
                .ToListAsync();

            return results;
        }
    }
}