using System.ComponentModel.DataAnnotations;
using static CatFoodSubscription.Common.ValidationConstants;
using static CatFoodSubscription.Common.ValidationConstants.ProductConstants;

namespace CatFoodSubscription.Web.ViewModels.Admin.Product
{
    public class AdminProductEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = errorRequiredMessage)]
        [StringLength(ProductNameMaxLength,
            MinimumLength = ProductNameMinLength,
            ErrorMessage = errorLengthMessage)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = errorRequiredMessage)]
        [StringLength(ProductDescriptionMaxLength,
            MinimumLength = ProductDescriptionMinLength,
            ErrorMessage = errorLengthMessage)]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = errorRequiredMessage)]
        public decimal Price { get; set; }
    }
}
