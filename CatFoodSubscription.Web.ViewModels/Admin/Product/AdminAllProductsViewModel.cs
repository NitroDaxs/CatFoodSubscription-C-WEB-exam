namespace CatFoodSubscription.Web.ViewModels.Admin.Product
{
    public class AdminAllProductsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public bool IsSubscription { get; set; }

        public bool IsDeleted { get; set; }
    }
}
