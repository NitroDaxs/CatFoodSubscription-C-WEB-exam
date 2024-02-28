using System.ComponentModel.DataAnnotations;
using static CatFoodSubscription.Common.ValidationConstants.StatusConstants;

namespace CatFoodSubscription.Data.Models
{
    public class Status
    {
        [Compare("Identification for order status")]
        [Key]
        public int Id { get; set; }

        [Compare("Name for the status")]
        [Required]
        [MaxLength(StatusNameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
