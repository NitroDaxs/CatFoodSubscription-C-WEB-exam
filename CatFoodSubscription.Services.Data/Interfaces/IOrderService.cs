using CatFoodSubscription.Web.ViewModels.Order;
using CatFoodSubscription.Web.ViewModels.SubscriptionBox;

namespace CatFoodSubscription.Services.Data.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderSummaryViewModel>> GetOrderSummaryAsync(string id);

        Task AddToCartAsync(OrderProductViewModel product, string id);

        Task<OrderProductViewModel> GetProductByIdAsync(int id);

        Task UpdateProductQuantityAsync(int productId, string action, string id);
        Task AddSubscriptionBoxToCartAsync(SubscriptionBoxAllViewModel subscriptionBox, string id);

        Task RemoveSubscriptionBoxAsync(int orderId);
    }
}
