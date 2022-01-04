using Microsoft.EntityFrameworkCore;
using RatTracker.Results;
using RatTracker.Schools;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace RatTracker.EntityFrameworkCore
{
    [ReplaceDbContext(typeof(IIdentityDbContext))]
    [ReplaceDbContext(typeof(ITenantManagementDbContext))]
    [ConnectionStringName("Default")]
    public class RatTrackerDbContext :
        AbpDbContext<RatTrackerDbContext>,
        IIdentityDbContext,
        ITenantManagementDbContext
    {
        /* Add DbSet properties for your Aggregate Roots / Entities here. */

        public DbSet<Result> Results { get; set; }
        public DbSet<School> Schools { get; set; }

        #region Entities from the modules

        /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
         * and replaced them for this DbContext. This allows you to perform JOIN
         * queries for the entities of these modules over the repositories easily. You
         * typically don't need that for other modules. But, if you need, you can
         * implement the DbContext interface of the needed module and use ReplaceDbContext
         * attribute just like IIdentityDbContext and ITenantManagementDbContext.
         *
         * More info: Replacing a DbContext of a module ensures that the related module
         * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
         */

        //Identity
        public DbSet<IdentityUser> Users { get; set; }

        public DbSet<IdentityRole> Roles { get; set; }
        public DbSet<IdentityClaimType> ClaimTypes { get; set; }
        public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
        public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
        public DbSet<IdentityLinkUser> LinkUsers { get; set; }

        // Tenant Management
        public DbSet<Tenant> Tenants { get; set; }

        public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

        #endregion Entities from the modules

        public RatTrackerDbContext(DbContextOptions<RatTrackerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */

            builder.ConfigurePermissionManagement();
            builder.ConfigureSettingManagement();
            builder.ConfigureBackgroundJobs();
            builder.ConfigureAuditLogging();
            builder.ConfigureIdentity();
            builder.ConfigureIdentityServer();
            builder.ConfigureFeatureManagement();
            builder.ConfigureTenantManagement();

            /* Configure your own tables/entities inside here */

            if (builder.IsHostDatabase())
            {
                builder.Entity<School>(b =>
                {
                    b.ToTable(RatTrackerConsts.DbTablePrefix + "Schools", RatTrackerConsts.DbSchema);
                    b.ConfigureByConvention();
                    b.Property(x => x.Name).HasColumnName(nameof(School.Name)).IsRequired().HasMaxLength(SchoolConsts.NameMaxLength);
                    b.Property(x => x.Address1).HasColumnName(nameof(School.Address1)).IsRequired().HasMaxLength(SchoolConsts.Address1MaxLength);
                    b.Property(x => x.Address2).HasColumnName(nameof(School.Address2)).HasMaxLength(SchoolConsts.Address2MaxLength);
                    b.Property(x => x.Address3).HasColumnName(nameof(School.Address3)).HasMaxLength(SchoolConsts.Address3MaxLength);
                    b.Property(x => x.City).HasColumnName(nameof(School.City)).IsRequired().HasMaxLength(SchoolConsts.CityMaxLength);
                    b.Property(x => x.PostalCode).HasColumnName(nameof(School.PostalCode)).IsRequired();
                });

                builder.Entity<Result>(b =>
                {
                    b.ToTable(RatTrackerConsts.DbTablePrefix + "Results", RatTrackerConsts.DbSchema);
                    b.ConfigureByConvention();
                    b.Property(x => x.TestDate).HasColumnName(nameof(Result.TestDate)).IsRequired();
                    b.Property(x => x.Age).HasColumnName(nameof(Result.Age)).IsRequired();
                    b.Property(x => x.Outcome).HasColumnName(nameof(Result.Outcome)).IsRequired();
                    b.HasOne<School>().WithMany().IsRequired().HasForeignKey(x => x.SchoolId);
                });
            }
        }
    }
}
