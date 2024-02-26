using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatFoodSubscription.Data.Models
{
    public class Order
    {
        [Comment("Identification for the order")]
        [Key]
        public int Id { get; set; }

        [Comment("Identification of the customer")]
        [Required]
        public int CustomerId { get; set; }

        [Comment("Customer of the order")]
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; } = null!;

        [Comment("Date of the order")]
        [Required]
        public DateTime OrderDate { get; set; }

        [Comment("Date of the shipment")]
        [Required]
        public DateTime ShippedDate { get; set; }

        [Comment("Indicates whether the order is a subscription")]
        [Required]
        public bool IsSubscription { get; set; }

        [Comment("Identification of the address")]
        public int AddressId { get; set; }

        [Comment("Address of the order")]
        [ForeignKey(nameof(AddressId))]
        public Address Address { get; set; } = null!;

        [Comment("Identification of the subscriptionBox")]
        public int? SubscriptionBoxId { get; set; }

        [Comment("The subscriptionBox")]
        [ForeignKey(nameof(SubscriptionBoxId))]
        public SubscriptionBox SubscriptionBox { get; set; } = null!;

        [Comment("Collection of products in the order")]
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
