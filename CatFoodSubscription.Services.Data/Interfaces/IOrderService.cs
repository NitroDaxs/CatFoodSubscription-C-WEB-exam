using CatFoodSubscription.Web.ViewModels.Order;

namespace CatFoodSubscription.Services.Data.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderSummaryViewModel>> GetOrderSummaryAsync(string id);

        Task AddToCartAsync(OrderProductViewModel product, bool purchaseType, string id);

        Task<OrderProductViewModel> GetProductByIdAsync(int id);

        Task UpdateProductQuantityAsync(int productId, string action, string id);
    }
}
