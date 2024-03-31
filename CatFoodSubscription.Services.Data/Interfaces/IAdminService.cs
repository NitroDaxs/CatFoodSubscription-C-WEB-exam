using CatFoodSubscription.Web.Areas.Admin.ViewModels;
using CatFoodSubscription.Web.ViewModels.Admin;

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
    }
}
