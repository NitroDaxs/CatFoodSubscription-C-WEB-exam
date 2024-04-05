namespace CatFoodSubscription.Web.ViewModels.Product
{
    public class ProductListViewModel
    {
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalItems / ItemsPerPage);
    }
}
