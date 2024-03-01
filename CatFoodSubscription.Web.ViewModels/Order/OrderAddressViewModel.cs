namespace CatFoodSubscription.Web.ViewModels.Order
{
    public class OrderAddressViewModel
    {
        public int Id { get; set; }

        public string Country { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Street { get; set; } = string.Empty;

        public int PostalCode { get; set; }
    }
}
