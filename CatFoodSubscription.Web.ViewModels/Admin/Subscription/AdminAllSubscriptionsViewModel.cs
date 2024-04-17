using CatFoodSubscription.Web.ViewModels.Admin.Product;

namespace CatFoodSubscription.Web.ViewModels.Admin.Subscription
{
    public class AdminAllSubscriptionsViewModel
    {
        public int OrderId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? RenewalDate { get; set; }
        public DateTime? RenewedDate { get; set; }

        public ICollection<AdminAllProductsViewModel> Products { get; set; } = new HashSet<AdminAllProductsViewModel>();
    }
}
