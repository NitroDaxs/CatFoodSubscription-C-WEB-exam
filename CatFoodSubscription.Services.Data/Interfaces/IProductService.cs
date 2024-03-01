using CatFoodSubscription.Web.ViewModels.Product;

namespace CatFoodSubscription.Services.Data.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductAllViewModel>> GetProductAllAsync();
        Task<IEnumerable<ProductAllViewModel>> GetProductSearchAsync(string query);
        Task<ProductDetailViewModel> GetProductByIdAsync(int id);

        Task<IEnumerable<ProductSearchViewModel>> GetProductSearchBarAsync(string query);
    }
}
