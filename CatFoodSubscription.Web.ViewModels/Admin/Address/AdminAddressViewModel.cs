namespace CatFoodSubscription.Web.ViewModels.Admin.Address
{
    public class AdminAddressViewModel
    {
        public int Id { get; set; }

        public string Country { get; set; } = null!;

        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;

        public string PostalCode { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
