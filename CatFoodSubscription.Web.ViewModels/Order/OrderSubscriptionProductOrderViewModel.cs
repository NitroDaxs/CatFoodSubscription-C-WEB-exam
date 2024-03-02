namespace CatFoodSubscription.Web.ViewModels.Order
{
    public class OrderSubscriptionProductOrderViewModel
    {
        public int Id { get; set; }

        public string CustomerId { get; set; } = null!;

        public DateTime OrderDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public int StatusId { get; set; }

        public OrderSubscriptionBoxViewModel? SubscriptionBox { get; set; }

        public ICollection<OrderProductViewModel> Products { get; set; } = new HashSet<OrderProductViewModel>();
    }
}
