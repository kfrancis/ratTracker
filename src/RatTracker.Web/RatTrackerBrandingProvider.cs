using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace RatTracker.Web
{
    [Dependency(ReplaceServices = true)]
    public class RatTrackerBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "RatTracker";
    }
}
