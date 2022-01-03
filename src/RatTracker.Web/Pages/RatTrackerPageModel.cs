using RatTracker.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace RatTracker.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class RatTrackerPageModel : AbpPageModel
    {
        protected RatTrackerPageModel()
        {
            LocalizationResourceType = typeof(RatTrackerResource);
        }
    }
}