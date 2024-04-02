using CatFoodSubscription.Web.ViewModels.Admin.Address;
using CatFoodSubscription.Web.ViewModels.Admin.Product;
using CatFoodSubscription.Web.ViewModels.Admin.SubsctiptionBox;

namespace CatFoodSubscription.Web.ViewModels.Admin.Order
{
    public class AdminOrderSummaryViewModel
    {
        public int Id { get; set; }

        public string CustomerId { get; set; } = null!;

        public AdminAddressViewModel Address { get; set; }

        public AdminSubscriptionBoxViewModel? SubscriptionBox { get; set; }

        public ICollection<AdminAllProductsViewModel> Products { get; set; } = new HashSet<AdminAllProductsViewModel>();
    }
}
