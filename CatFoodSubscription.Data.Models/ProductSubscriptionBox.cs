using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatFoodSubscription.Data.Models
{
    public class ProductSubscriptionBox
    {
        [Required]
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;

        [Required]
        public int SubscriptionBoxId { get; set; }

        [ForeignKey(nameof(SubscriptionBoxId))]
        public SubscriptionBox SubscriptionBox { get; set; } = null!;
    }
}
