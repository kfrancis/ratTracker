using Volo.Abp.Modularity;

[assembly: CLSCompliant(false)]

namespace RatTracker
{
    [DependsOn(
        typeof(RatTrackerApplicationModule),
        typeof(RatTrackerDomainTestModule)
        )]
    public class RatTrackerApplicationTestModule : AbpModule
    {
    }
}
