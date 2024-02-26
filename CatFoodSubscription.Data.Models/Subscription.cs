using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatFoodSubscription.Data.Models
{
    public class Subscription
    {
        [Comment("Identification of the subscription")]
        [Key]
        public int Id { get; set; }

        [Comment("Identification of the customer")]
        [Required]
        public int CustomerId { get; set; }

        [Comment("The customer")]
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; } = null!;
    }
}
