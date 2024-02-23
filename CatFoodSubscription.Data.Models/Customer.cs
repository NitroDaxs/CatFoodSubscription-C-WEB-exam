using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CatFoodSubscription.Common.ValidationConstants.CustomerConstants;

namespace CatFoodSubscription.Data.Models
{
    public class Customer
    {
        [Comment("Identification for the customer")]
        [Key]
        public int Id { get; set; }

        [Comment("First name of the customer")]
        [Required]
        [MaxLength(CustomerFirstNameMaxLength)]
        public string FirstName { get; set; } = string.Empty;

        [Comment("Last name of the customer")]
        [Required]
        [MaxLength(CustomerLastNameMaxLength)]
        public string LastName { get; set; } = string.Empty;

        [Comment("Phone number of the customer")]
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Comment("Address Id of the customer")]
        [Required]
        public int AddressId { get; set; }

        [Comment("Address of the customer")]
        [ForeignKey(nameof(AddressId))]
        public Address Address { get; set; } = null!;

        [Comment("Identification for the user")]
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Comment("The user")]
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;
    }
}
