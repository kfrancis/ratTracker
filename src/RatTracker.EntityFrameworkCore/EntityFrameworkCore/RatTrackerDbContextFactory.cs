using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace RatTracker.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */

    public class RatTrackerDbContextFactory : IDesignTimeDbContextFactory<RatTrackerDbContext>
    {
        public RatTrackerDbContext CreateDbContext(string[] args)
        {
            RatTrackerEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<RatTrackerDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"), opt => opt.UseNetTopologySuite());

            return new RatTrackerDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../RatTracker.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
