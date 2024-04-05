using System.ComponentModel.DataAnnotations;

namespace CatFoodSubscription.Web.ViewModels.Order
{
    public class OrderCheckOutFormViewModel
    {
        public int OrderId { get; set; }

        public string CustomerId { get; set; } = null!;

        public OrderSubscriptionBoxViewModel? SubscriptionBox { get; set; }

        public DateTime OrderDate { get; set; }

        [Required]
        public OrderAddressViewModel Address { get; set; }
        public ICollection<OrderProductViewModel> Products { get; set; } = new HashSet<OrderProductViewModel>();


    }
}
