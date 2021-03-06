using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace RatTracker
{
    public class RatTrackerTestDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public Task SeedAsync(DataSeedContext context)
        {
            /* Seed additional test data... */

            return Task.CompletedTask;
        }
    }
}
