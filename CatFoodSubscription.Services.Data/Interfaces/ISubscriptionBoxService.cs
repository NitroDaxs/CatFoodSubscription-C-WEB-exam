using CatFoodSubscription.Web.ViewModels.SubscriptionBox;

namespace CatFoodSubscription.Services.Data.Interfaces
{
    public interface ISubscriptionBoxService
    {
        Task<IEnumerable<SubscriptionBoxAllViewModel>> GetAllAsync();
    }
}
