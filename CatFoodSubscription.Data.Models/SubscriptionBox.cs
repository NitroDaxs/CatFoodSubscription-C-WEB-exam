using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static CatFoodSubscription.Common.ValidationConstants.SubscriptionBoxConstants;

namespace CatFoodSubscription.Data.Models
{
    public class SubscriptionBox
    {
        [Comment("Identification of the subscriptionBox")]
        [Key]
        public int Id { get; set; }

        [Comment("Name of the subscriptionBox")]
        [Required]
        [MaxLength(SubscriptionBoxNameMaxLength)]
        public string Name { get; set; } = null!;

        [Comment("Path for subscriptionBox image")]
        public string? ImageUrl { get; set; }

        [Comment("Name of the subscriptionBox")]
        [Required]
        [MaxLength(SubscriptionBoxDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Comment("Price of the subscriptionBox")]
        public decimal Price { get; set; }

        [Comment("Collection of products in the subscriptionBox")]
        public ICollection<ProductSubscriptionBox> ProductSubscriptionBoxes { get; set; } = new HashSet<ProductSubscriptionBox>();
    }
}
