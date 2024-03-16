namespace CatFoodSubscription.Web.ViewModels.Order
{
    public class OrderSummaryViewModel
    {
        public int Id { get; set; }

        public string CustomerId { get; set; } = null!;

        public OrderAddressViewModel Address { get; set; }

        public OrderSubscriptionBoxViewModel? SubscriptionBox { get; set; }

        public ICollection<OrderProductViewModel> Products { get; set; } = new HashSet<OrderProductViewModel>();
    }
}
