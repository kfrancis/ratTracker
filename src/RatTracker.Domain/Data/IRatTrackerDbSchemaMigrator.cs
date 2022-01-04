namespace RatTracker.Data
{
    public interface IRatTrackerDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
