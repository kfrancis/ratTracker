using RatTracker.Schools;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.BackgroundWorkers.Hangfire;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace RatTracker
{
    [DependsOn(
        typeof(RatTrackerDomainModule),
        typeof(AbpAccountApplicationModule),
        typeof(RatTrackerApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(AbpSettingManagementApplicationModule),
        typeof(AbpBackgroundWorkerHangfireModule)
        )]
    public class RatTrackerApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<RatTrackerApplicationModule>();
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            context.AddBackgroundWorker<SchoolGeocodingWorker>();
        }
    }
}
