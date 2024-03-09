namespace CatFoodSubscription.Web.ViewModels.Product
{
    public class PaginatedProductsViewModel
    {
        public IEnumerable<ProductAllViewModel> Products { get; set; }
        public ProductListViewModel PaginationInfo { get; set; }
    }
}
