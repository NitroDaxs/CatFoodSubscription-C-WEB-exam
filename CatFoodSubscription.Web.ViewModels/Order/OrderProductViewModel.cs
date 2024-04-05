namespace CatFoodSubscription.Web.ViewModels.Order
{
    public class OrderProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public int Quantity { get; set; }

        public bool IsSubscription { get; set; }
    }
}
