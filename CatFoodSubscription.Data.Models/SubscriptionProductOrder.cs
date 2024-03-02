using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatFoodSubscription.Data.Models
{
    public class SubscriptionProductOrder
    {
        [Comment("Identification of the product")]
        [Required]
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;

        [Comment("Identification of the order")]
        [Required]
        public int OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; } = null!;

        [Comment("Quantity of the product")]
        [Required]
        public int Quantity { get; set; } = 1;
    }
}
