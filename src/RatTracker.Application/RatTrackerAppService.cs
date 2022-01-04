using RatTracker.Localization;
using Volo.Abp.Application.Services;

namespace RatTracker
{
    /* Inherit your application services from this class.
     */

    public abstract class RatTrackerAppService : ApplicationService
    {
        protected RatTrackerAppService()
        {
            LocalizationResource = typeof(RatTrackerResource);
        }
    }
}
