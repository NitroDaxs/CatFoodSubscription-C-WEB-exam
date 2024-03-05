using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static CatFoodSubscription.Common.ValidationConstants.AddressConstants;

namespace CatFoodSubscription.Data.Models
{
    public class Address
    {
        [Comment("Identification for the address")]
        [Key]
        public int Id { get; set; }

        [Comment("Country of the address")]
        [Required]
        [MaxLength(AddressCountryMaxLength)]
        public string Country { get; set; } = null!;

        [Comment("City of the address")]
        [Required]
        [MaxLength(AddressCityMaxLength)]
        public string City { get; set; } = null!;

        [Comment("Street of the address")]
        [Required]
        [MaxLength(AddressStreetMaxLength)]
        public string Street { get; set; } = null!;

        [Comment("PostalCode of the address")]
        public int PostalCode { get; set; }


        [Comment("PhoneNumber for the address")]
        public string PhoneNumber { get; set; } = null!;
    }
}
