using Hangfire;
using Hangfire.Console;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            var configuration = context.Services.GetConfiguration();
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
            context.Services.AddHangfire(config =>
            {
                config.UseConsole();
                config.UseSimpleAssemblyNameTypeSerializer();
                config.UseRecommendedSerializerSettings();
                config.UseSqlServerStorage(configuration.GetConnectionString("Default"));
            });
            JobStorage.Current = new SqlServerStorage(configuration.GetConnectionString("Default"));
        }
    }
}
