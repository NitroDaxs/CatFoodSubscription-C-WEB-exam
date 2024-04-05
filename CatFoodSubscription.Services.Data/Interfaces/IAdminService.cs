using CatFoodSubscription.Web.ViewModels.Admin.Category;
using CatFoodSubscription.Web.ViewModels.Admin.Order;
using CatFoodSubscription.Web.ViewModels.Admin.Product;
using CatFoodSubscription.Web.ViewModels.Admin.Status;

namespace CatFoodSubscription.Services.Data.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<AdminAllProductsViewModel>> GetAdminProductAllAsync();
        Task<IEnumerable<AdminAllOrdersViewModel>> GetAdminOrderAllAsync();
        Task<IEnumerable<AdminAllProductsViewModel>> GetAdminProductBySearchAsync(string query);

        Task<AdminProductEditViewModel> GetAdminProductByIdAsync(int id);
        Task<AdminDeleteViewModel> GetAdminDeleteProductByIdAsync(int id);
        Task<AdminRestoreViewModel> GetAdminRestoreProductByIdAsync(int id);
        Task EditAdminProductByIdAsync(AdminProductEditViewModel model);
        Task ConfirmAdminDeleteProductAsync(AdminDeleteViewModel model);
        Task ConfirmAdminRestoreProductAsync(AdminRestoreViewModel model);
        Task<IEnumerable<AdminAllOrdersViewModel>> GetAdminOrderByIdAsync(int id);
        Task<AdminOrderChangeStatusViewModel> GetAdminOrderByIdChangeStatusAsync(int id);
        Task UpdateAdminOrderStatus(AdminOrderChangeStatusViewModel model);

        Task<IEnumerable<AdminStatusViewModel>> GetAdminOrderStatusesAsync();
        Task<IEnumerable<AdminCategoryViewModel>> GetAdminProductCategoriesAsync();

        Task<AdminOrderSummaryViewModel> OrderSummaryByIdAsync(int id);
    }
}
