using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static CatFoodSubscription.Common.ValidationConstants;
using static CatFoodSubscription.Common.ValidationConstants.AddressConstants;

namespace CatFoodSubscription.Web.ViewModels.Order
{
    public class OrderAddressViewModel
    {
        public int Id { get; set; }

        [DisplayName("Country")]
        [Required(ErrorMessage = errorRequiredMessage)]
        [StringLength(AddressCountryMaxLength,
            MinimumLength = AddressCountryMinLength,
            ErrorMessage = errorLengthMessage)]
        public string Country { get; set; } = null!;


        [DisplayName("City")]
        [Required(ErrorMessage = errorRequiredMessage)]
        [StringLength(AddressCityMaxLength,
            MinimumLength = AddressCityMinLength,
            ErrorMessage = errorLengthMessage)]
        public string City { get; set; } = null!;

        [DisplayName("Street")]
        [Required(ErrorMessage = errorRequiredMessage)]
        [StringLength(AddressStreetMaxLength,
            MinimumLength = AddressStreetMinLength,
            ErrorMessage = errorLengthMessage)]
        public string Street { get; set; } = null!;

        [DisplayName("Postal Code")]
        [Required(ErrorMessage = errorRequiredMessage)]
        [StringLength(AddressPostalCodeMaxLength,
            MinimumLength = AddressPostalCodeMinLength,
            ErrorMessage = errorLengthMessage)]
        public string PostalCode { get; set; } = null!;

        [DisplayName("Phone Number")]
        [Required(ErrorMessage = errorRequiredMessage)]
        [RegularExpression(PhoneNumberRegEx,
            ErrorMessage = PhoneNumberErrorMessage)]
        public string PhoneNumber { get; set; } = null!;

        [DisplayName("First Name")]
        [Required(ErrorMessage = errorRequiredMessage)]
        [StringLength(CustomerFirstNameMaxLength,
            MinimumLength = CustomerFirstNameMinLength,
            ErrorMessage = errorLengthMessage)]
        public string FirstName { get; set; } = null!;

        [DisplayName("Last Name")]
        [Required(ErrorMessage = errorRequiredMessage)]
        [StringLength(CustomerLastNameMaxLength,
            MinimumLength = CustomerLastNameMinLength,
            ErrorMessage = errorLengthMessage)]
        public string LastName { get; set; } = null!;

        [DisplayName("Email")]
        [Required(ErrorMessage = errorRequiredMessage)]
        [RegularExpression(EmailRegEx,
            ErrorMessage = EmailErrorMessage)]
        public string Email { get; set; } = null!;
    }
}
