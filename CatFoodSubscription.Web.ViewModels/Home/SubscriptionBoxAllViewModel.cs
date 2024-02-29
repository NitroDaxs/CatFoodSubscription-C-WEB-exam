namespace CatFoodSubscription.Web.ViewModels.Home
{
    public class SubscriptionBoxAllViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public ICollection<ProductSubscriptionBoxViewModel> ProductSubscriptionBoxes { get; set; } = new HashSet<ProductSubscriptionBoxViewModel>();
    }
}
