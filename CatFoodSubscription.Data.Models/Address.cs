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
        public string Country { get; set; } = string.Empty;

        [Comment("City of the address")]
        [Required]
        [MaxLength(AddressCityMaxLength)]
        public string City { get; set; } = string.Empty;

        [Comment("Street of the address")]
        [Required]
        [MaxLength(AddressStreetMaxLength)]
        public string Street { get; set; } = string.Empty;

        [Comment("PostalCode of the address")]
        public int PostalCode { get; set; }
    }
}
