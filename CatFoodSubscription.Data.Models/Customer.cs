using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static CatFoodSubscription.Common.ValidationConstants.CustomerConstants;

namespace CatFoodSubscription.Data.Models
{
    public class Customer : IdentityUser<string>
    {
        public Customer()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Comment("First name of the customer")]
        [MaxLength(CustomerFirstNameMaxLength)]
        public string? FirstName { get; set; }

        [Comment("Last name of the customer")]
        [MaxLength(CustomerLastNameMaxLength)]
        public string? LastName { get; set; }

        [Comment("Specifies if the customer account is deleted")]
        public bool IsDeleted { get; set; } = false;

        [Comment("Collection of all orders")]
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
