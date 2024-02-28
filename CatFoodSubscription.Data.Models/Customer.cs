using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CatFoodSubscription.Common.ValidationConstants.CustomerConstants;

namespace CatFoodSubscription.Data.Models
{
    public class Customer : IdentityUser<string>
    {
        [Comment("Identification for the customer")]
        [Key]
        public override string Id { get => base.Id; set => base.Id = value; }

        [Comment("First name of the customer")]
        [MaxLength(CustomerFirstNameMaxLength)]
        public string? FirstName { get; set; }

        [Comment("Last name of the customer")]
        [MaxLength(CustomerLastNameMaxLength)]
        public string? LastName { get; set; }

        [Comment("Phone number of the customer")]
        public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }

        [Comment("Address Id of the customer")]
        [Required]
        public int AddressId { get; set; }

        [Comment("Specifies if the customer account is deleted")]
        public bool IsDeleted { get; set; } = false;

        [Comment("Address of the customer")]
        [ForeignKey(nameof(AddressId))]
        public Address Address { get; set; } = null!;

        [Comment("Collection of all orders")]
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
