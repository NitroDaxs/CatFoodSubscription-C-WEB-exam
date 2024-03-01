using Microsoft.EntityFrameworkCore;
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

        [Comment("Quantity of the product")]
        [Required]
        public int Quantity { get; set; }

        [Comment("Indicates whether the product is a subscription")]
        [Required]
        public bool IsSubscription { get; set; } = false;

        [Comment("Price of the product")]
        [Required]
        public decimal Price { get; set; }

        [Comment("Path leading to the product's image")]
        public string? ImageUrl { get; set; }

        [Comment("Identification for the category of the product")]
        [Required]
        public int CategoryId { get; set; }

        [Comment("Category of the product")]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        [Comment("Collection for the mapping table")]
        public ICollection<ProductSubscriptionBox> ProductSubscriptionBoxes { get; set; } = new HashSet<ProductSubscriptionBox>();
    }
}
