using CatFoodSubscription.Web.ViewModels.Admin.Status;

namespace CatFoodSubscription.Web.ViewModels.Admin.Order
{
    public class AdminOrderChangeStatusViewModel
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }

        public int StatusId { get; set; }

        public IEnumerable<AdminStatusViewModel> Status { get; set; } = new HashSet<AdminStatusViewModel>();
    }
}
