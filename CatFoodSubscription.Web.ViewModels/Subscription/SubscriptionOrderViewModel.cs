using CatFoodSubscription.Web.ViewModels.Order;

namespace CatFoodSubscription.Web.ViewModels.Subscription
{
    public class SubscriptionOrderViewModel
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public ICollection<OrderProductViewModel> Products { get; set; } = new HashSet<OrderProductViewModel>();

        public OrderSubscriptionBoxViewModel? SubscriptionBox { get; set; }
    }
}
