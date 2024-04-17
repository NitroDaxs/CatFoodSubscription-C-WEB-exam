namespace CatFoodSubscription.Web.ViewModels.Admin.Order
{
    public class AdminAllOrdersViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Status { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

    }
}
