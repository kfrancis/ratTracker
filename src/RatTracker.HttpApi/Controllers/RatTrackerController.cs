using RatTracker.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace RatTracker.Controllers
{
    /* Inherit your controllers from this class.
     */

    public abstract class RatTrackerController : AbpControllerBase
    {
        protected RatTrackerController()
        {
            LocalizationResource = typeof(RatTrackerResource);
        }
    }
}
