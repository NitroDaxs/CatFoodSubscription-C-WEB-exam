using CatFoodSubscription.Web.ViewModels.Order;
using CatFoodSubscription.Web.ViewModels.SubscriptionBox;

namespace CatFoodSubscription.Services.Data.Interfaces
{
    public interface IOrderService
    {
        Task<OrderSummaryViewModel> GetOrderSummaryAsync(string id);

        Task<OrderCheckOutFormViewModel> GetCheckOutSummaryAsync(string id);
        Task ProcessOrderAsync(OrderCheckOutFormViewModel orderViewModel, string id);

        Task AddToCartAsync(OrderProductViewModel product, string id);

        Task<OrderProductViewModel> GetProductByIdAsync(int id);

        Task UpdateProductQuantityAsync(int productId, string action, string id);
        Task AddSubscriptionBoxToCartAsync(SubscriptionBoxAllViewModel subscriptionBox, string id);

        Task RemoveSubscriptionBoxAsync(int orderId);
        Task<IEnumerable<OrderAllViewModel>> GetAllOrdersByIdAsync(string id);

        Task<OrderSummaryViewModel> OrderSummaryAsync(int id);
    }
}
