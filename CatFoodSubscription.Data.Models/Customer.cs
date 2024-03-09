using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CatFoodSubscription.Data.Models
{
    public class Customer : IdentityUser<string>
    {
        public Customer()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Comment("Specifies if the customer account is deleted")]
        public bool IsDeleted { get; set; } = false;

        [Comment("Collection of all orders")]
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
