using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement;

namespace RatTracker.Data
{
    public class RatTrackerDbMigrationService : ITransientDependency
    {
        public ILogger<RatTrackerDbMigrationService> Logger { get; set; }

        private readonly IDataSeeder _dataSeeder;
        private readonly IEnumerable<IRatTrackerDbSchemaMigrator> _dbSchemaMigrators;
        private readonly ITenantRepository _tenantRepository;
        private readonly ICurrentTenant _currentTenant;

        public RatTrackerDbMigrationService(
            IDataSeeder dataSeeder,
            IEnumerable<IRatTrackerDbSchemaMigrator> dbSchemaMigrators,
            ITenantRepository tenantRepository,
            ICurrentTenant currentTenant)
        {
            _dataSeeder = dataSeeder;
            _dbSchemaMigrators = dbSchemaMigrators;
            _tenantRepository = tenantRepository;
            _currentTenant = currentTenant;

            Logger = NullLogger<RatTrackerDbMigrationService>.Instance;
        }

        public async Task MigrateAsync()
        {
            var initialMigrationAdded = AddInitialMigrationIfNotExist();

            if (initialMigrationAdded)
            {
                return;
            }

            Logger.LogInformation("Started database migrations...");

            await MigrateDatabaseSchemaAsync().ConfigureAwait(false);
            await SeedDataAsync().ConfigureAwait(false);

            Logger.LogInformation($"Successfully completed host database migrations.");

            var tenants = await _tenantRepository.GetListAsync(includeDetails: true).ConfigureAwait(false);

            var migratedDatabaseSchemas = new HashSet<string>();
            foreach (var tenant in tenants)
            {
                using (_currentTenant.Change(tenant.Id))
                {
                    if (tenant.ConnectionStrings.Any())
                    {
                        var tenantConnectionStrings = tenant.ConnectionStrings
                            .Select(x => x.Value)
                            .ToList();

                        if (!migratedDatabaseSchemas.IsSupersetOf(tenantConnectionStrings))
                        {
                            await MigrateDatabaseSchemaAsync(tenant).ConfigureAwait(false);

                            migratedDatabaseSchemas.AddIfNotContains(tenantConnectionStrings);
                        }
                    }

                    await SeedDataAsync(tenant).ConfigureAwait(false);
                }

                var tenantName = tenant.Name;
                Logger.LogInformation("Successfully completed {TenantName} tenant database migrations.", tenantName);
            }

            Logger.LogInformation("Successfully completed all database migrations.");
            Logger.LogInformation("You can safely end this process...");
        }

        private async Task MigrateDatabaseSchemaAsync(Tenant? tenant = null)
        {
            string tenantName = (tenant == null ? "host" : tenant.Name + " tenant");
            Logger.LogInformation("Migrating schema for {TenantName} database...", tenantName);

            foreach (var migrator in _dbSchemaMigrators)
            {
                await migrator.MigrateAsync().ConfigureAwait(false);
            }
        }

        private async Task SeedDataAsync(Tenant? tenant = null)
        {
            string tenantName = (tenant == null ? "host" : tenant.Name + " tenant");
            Logger.LogInformation("Executing {TenantName} database seed...", tenantName);

            await _dataSeeder.SeedAsync(new DataSeedContext(tenant?.Id)
                .WithProperty(IdentityDataSeedContributor.AdminEmailPropertyName, IdentityDataSeedContributor.AdminEmailDefaultValue)
                .WithProperty(IdentityDataSeedContributor.AdminPasswordPropertyName, IdentityDataSeedContributor.AdminPasswordDefaultValue)
            ).ConfigureAwait(false);
        }

        private bool AddInitialMigrationIfNotExist()
        {
            try
            {
                if (!DbMigrationsProjectExists())
                {
                    return false;
                }
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                return false;
            }

            try
            {
                if (!MigrationsFolderExists())
                {
                    AddInitialMigration();
                    return true;
                }
                else
                {
                    return false;
                }
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception e)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                Logger.LogWarning("Couldn't determinate if any migrations exist: {Message}", e.Message);
                return false;
            }
        }

        private static bool DbMigrationsProjectExists()
        {
            var dbMigrationsProjectFolder = GetEntityFrameworkCoreProjectFolderPath();

            return dbMigrationsProjectFolder != null;
        }

        private static bool MigrationsFolderExists()
        {
            var dbMigrationsProjectFolder = GetEntityFrameworkCoreProjectFolderPath();
            if (dbMigrationsProjectFolder != null)
            {
                return Directory.Exists(Path.Combine(dbMigrationsProjectFolder, "Migrations"));
            }
            else
            {
                return false;
            }
        }

        private void AddInitialMigration()
        {
            Logger.LogInformation("Creating initial migration...");

            string argumentPrefix;
            string fileName;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                argumentPrefix = "-c";
                fileName = "/bin/bash";
            }
            else
            {
                argumentPrefix = "/C";
                fileName = "cmd.exe";
            }

            var procStartInfo = new ProcessStartInfo(fileName,
                $"{argumentPrefix} \"abp create-migration-and-run-migrator \"{GetEntityFrameworkCoreProjectFolderPath()}\"\""
            );

            try
            {
                Process.Start(procStartInfo);
            }
            catch (Exception)
            {
#pragma warning disable CA2201 // Do not raise reserved exception types
                throw new Exception("Couldn't run ABP CLI...");
#pragma warning restore CA2201 // Do not raise reserved exception types
            }
        }

        private static string? GetEntityFrameworkCoreProjectFolderPath()
        {
            var slnDirectoryPath = GetSolutionDirectoryPath();

            if (slnDirectoryPath == null)
            {
#pragma warning disable CA2201 // Do not raise reserved exception types
                throw new Exception("Solution folder not found!");
#pragma warning restore CA2201 // Do not raise reserved exception types
            }

            var srcDirectoryPath = Path.Combine(slnDirectoryPath, "src");

            return Directory.GetDirectories(srcDirectoryPath)
                .FirstOrDefault(d => d.EndsWith(".EntityFrameworkCore", true, CultureInfo.CurrentCulture));
        }

        private static string? GetSolutionDirectoryPath()
        {
            var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());

            while (currentDirectory != null && Directory.GetParent(currentDirectory.FullName) != null)
            {
                currentDirectory = Directory.GetParent(currentDirectory.FullName);

                if (currentDirectory == null) { continue; }

                if (Directory.GetFiles(currentDirectory.FullName).FirstOrDefault(f => f.EndsWith(".sln", true, CultureInfo.CurrentCulture)) != null)
                {
                    return currentDirectory.FullName;
                }
            }

            return null;
        }
    }
}
