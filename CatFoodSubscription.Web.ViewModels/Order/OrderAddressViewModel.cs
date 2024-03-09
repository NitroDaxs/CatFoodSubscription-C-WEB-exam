using System.ComponentModel.DataAnnotations;

namespace CatFoodSubscription.Web.ViewModels.Order
{
    public class OrderAddressViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Country { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string Street { get; set; } = string.Empty;
        [Required]
        public int PostalCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; }
    }
}
