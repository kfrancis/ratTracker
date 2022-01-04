using RatTracker.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

[assembly: CLSCompliant(false)]

namespace RatTracker.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(RatTrackerEntityFrameworkCoreModule),
        typeof(RatTrackerApplicationContractsModule)
        )]
    public class RatTrackerDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
