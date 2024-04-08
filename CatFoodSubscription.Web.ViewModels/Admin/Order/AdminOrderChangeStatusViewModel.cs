using CatFoodSubscription.Web.ViewModels.Admin.Status;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CatFoodSubscription.Web.ViewModels.Admin.Order
{
    public class AdminOrderChangeStatusViewModel
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }

        [DisplayName("Select Category:")]
        [Required(ErrorMessage = Common.ValidationConstants.errorRequiredMessage)]
        public int? StatusId { get; set; }

        public IEnumerable<AdminStatusViewModel> Status { get; set; } = new HashSet<AdminStatusViewModel>();
    }
}
