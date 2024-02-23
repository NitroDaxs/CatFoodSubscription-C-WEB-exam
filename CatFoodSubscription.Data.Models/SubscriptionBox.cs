using System.ComponentModel.DataAnnotations;

namespace CatFoodSubscription.Data.Models
{
    public class SubscriptionBox
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Discount { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
