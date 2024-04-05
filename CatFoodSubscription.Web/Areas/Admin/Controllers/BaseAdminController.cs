using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CatFoodSubscription.Common.ValidationConstants.Roles;

namespace CatFoodSubscription.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = AdminRoleName)]
    public class BaseAdminController : Controller
    {
    }
}
