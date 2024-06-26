﻿using Microsoft.EntityFrameworkCore;
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
        public string CustomerId { get; set; } = null!;

        [Comment("Customer of the order")]
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; } = null!;

        [Comment("Date of the order")]
        [Required]
        public DateTime OrderDate { get; set; }

        public DateTime? RenewalDate { get; set; }
        public DateTime? RenewedDate { get; set; }

        [Comment("Date of the shipment")]
        public DateTime? ShippedDate { get; set; }

        [Comment("Date of the arrival")]
        public DateTime? ArrivalDate { get; set; }

        [Required]
        public int StatusId { get; set; }

        [ForeignKey(nameof(StatusId))]
        public Status Status { get; set; } = null!;

        [Comment("Identification of the address")]
        public int? AddressId { get; set; }

        [Comment("Address of the order")]
        [ForeignKey(nameof(AddressId))]
        public Address? Address { get; set; }

        [Comment("Identification of the subscriptionBox")]
        public int? SubscriptionBoxId { get; set; }

        [Comment("Indicates if the subscription is canceled")]
        public bool IsSubscriptionCanceled { get; set; } = false;

        [Comment("The subscriptionBox")]
        [ForeignKey(nameof(SubscriptionBoxId))]
        public SubscriptionBox? SubscriptionBox { get; set; } = null!;

        [Comment("Collection of orders containing the product")]
        public ICollection<ProductOrder> ProductsOrders { get; set; } = new HashSet<ProductOrder>();
    }
}
