using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RatTracker.Data;
using Volo.Abp.DependencyInjection;

namespace RatTracker.EntityFrameworkCore
{
    public class EntityFrameworkCoreRatTrackerDbSchemaMigrator
        : IRatTrackerDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreRatTrackerDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the RatTrackerDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<RatTrackerDbContext>()
                .Database
                .MigrateAsync().ConfigureAwait(false);
        }
    }
}
