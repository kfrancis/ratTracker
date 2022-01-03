using System.Threading.Tasks;

namespace RatTracker.Data
{
    public interface IRatTrackerDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
