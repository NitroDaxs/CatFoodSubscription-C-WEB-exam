using CatFoodSubscription.Web.ViewModels.Subscription;

namespace CatFoodSubscription.Services.Data.Interfaces
{
    public interface ISubscriptionService
    {
        Task<SubscriptionAllViewModel> GetOrderSubscriptionProductAsync(string id);

        Task CancelSubscription(int id);
    }
}
