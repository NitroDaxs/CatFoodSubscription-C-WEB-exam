namespace CatFoodSubscription.Web.ViewModels.Product
{
    public class ProductDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string Category { get; set; } = null!;
    }
}

