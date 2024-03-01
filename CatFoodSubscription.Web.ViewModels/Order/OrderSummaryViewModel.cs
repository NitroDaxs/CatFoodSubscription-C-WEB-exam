namespace CatFoodSubscription.Web.ViewModels.Order
{
    public class OrderSummaryViewModel
    {
        public int Id { get; set; }

        public string CustomerId { get; set; } = null!;

        public DateTime OrderDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public bool IsSubscription { get; set; } = false;

        public int StatusId { get; set; }

        public string Status { get; set; } = null!;

        public OrderAddressViewModel Address { get; set; } = null!;

        public OrderSubscriptionBoxViewModel SubscriptionBox { get; set; } = null!;
        public ICollection<OrderProductViewModel> Products { get; set; } = new HashSet<OrderProductViewModel>();
    }
}
