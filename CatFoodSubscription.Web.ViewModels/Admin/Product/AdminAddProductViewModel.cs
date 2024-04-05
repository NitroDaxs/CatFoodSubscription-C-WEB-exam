using CatFoodSubscription.Web.ViewModels.Admin.Category;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static CatFoodSubscription.Common.ValidationConstants;
using static CatFoodSubscription.Common.ValidationConstants.ProductConstants;

namespace CatFoodSubscription.Web.ViewModels.Admin.Product
{
    public class AdminAddProductViewModel
    {
        public int Id { get; set; }

        [DisplayName("Product Name:")]
        [Required(ErrorMessage = errorRequiredMessage)]
        [StringLength(ProductNameMaxLength, MinimumLength = ProductNameMinLength, ErrorMessage = errorLengthMessage)]
        public string Name { get; set; } = null!;

        [DisplayName("Product Description:")]
        [Required(ErrorMessage = errorRequiredMessage)]
        [StringLength(ProductDescriptionMaxLength, MinimumLength = ProductDescriptionMinLength, ErrorMessage = errorLengthMessage)]
        public string Description { get; set; } = null!;

        [DisplayName("Product Price:")]
        [Range(double.Epsilon, double.MaxValue, ErrorMessage = ProductPriceMoreThan0ErrorMsg)]
        [Required(ErrorMessage = errorRequiredMessage)]
        public decimal Price { get; set; }

        [DisplayName("Product Image:")]
        public string? ImageUrl { get; set; }

        [DisplayName("Product Type:")]
        [Required(ErrorMessage = errorRequiredMessage)]
        public bool IsSubscription { get; set; } = true;

        [DisplayName("Select Category:")]
        [Required(ErrorMessage = errorRequiredMessage)]
        public int? CategoryId { get; set; }
        public IEnumerable<AdminCategoryViewModel> Status { get; set; } = new HashSet<AdminCategoryViewModel>();
    }
}
