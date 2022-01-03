using Volo.Abp.Modularity;

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