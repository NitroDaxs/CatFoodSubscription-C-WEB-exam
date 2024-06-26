﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CatFoodSubscription.Common.ValidationConstants.ProductConstants;

namespace CatFoodSubscription.Data.Models
{
    public class Product
    {
        [Comment("Identification for the product")]
        [Key]
        public int Id { get; set; }

        [Comment("Name of the product")]
        [Required]
        [MaxLength(ProductNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Comment("Description of the product")]
        [Required]
        [MaxLength(ProductDescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        [Comment("Price of the product")]
        [Required]
        public decimal Price { get; set; }

        [Comment("Path leading to the product's image")]
        public string? ImageUrl { get; set; }

        [Comment("Indicates if the product is subscription based")]
        [Required]
        public bool IsSubscription { get; set; } = true;

        [Comment("Identification for the category of the product")]
        [Required]
        public int CategoryId { get; set; }

        [Comment("Category of the product")]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        [Comment("Indicates if the product is deleted")]
        public bool IsDeleted { get; set; } = false;

        [Comment("Collection of orders containing the product")]
        public ICollection<ProductOrder> ProductsOrders { get; set; } = new HashSet<ProductOrder>();

        [Comment("Collection for the mapping table")]
        public ICollection<ProductSubscriptionBox> ProductSubscriptionBoxes { get; set; } = new HashSet<ProductSubscriptionBox>();
    }
}
