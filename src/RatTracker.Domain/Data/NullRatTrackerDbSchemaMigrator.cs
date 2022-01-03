using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace RatTracker.Data
{
    /* This is used if database provider does't define
     * IRatTrackerDbSchemaMigrator implementation.
     */
    public class NullRatTrackerDbSchemaMigrator : IRatTrackerDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}