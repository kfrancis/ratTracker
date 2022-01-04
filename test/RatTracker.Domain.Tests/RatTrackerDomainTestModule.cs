using RatTracker.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace RatTracker
{
    [DependsOn(
        typeof(RatTrackerEntityFrameworkCoreTestModule)
        )]
    public class RatTrackerDomainTestModule : AbpModule
    {
    }
}
