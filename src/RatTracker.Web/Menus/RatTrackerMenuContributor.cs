using System.Threading.Tasks;
using RatTracker.Localization;
using RatTracker.MultiTenancy;
using RatTracker.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace RatTracker.Web.Menus
{
    public class RatTrackerMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var administration = context.Menu.GetAdministration();
            var l = context.GetLocalizer<RatTrackerResource>();

            context.Menu.Items.Insert(
                0,
                new ApplicationMenuItem(
                    RatTrackerMenus.Home,
                    l["Menu:Home"],
                    "~/",
                    icon: "fas fa-home",
                    order: 0
                )
            );

            if (MultiTenancyConsts.IsEnabled)
            {
                administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
            }
            else
            {
                administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
            }

            administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
            administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

            context.Menu.AddItem(
                new ApplicationMenuItem(
                    RatTrackerMenus.Schools,
                    l["Menu:Schools"],
                    url: "/Schools",
                    icon: "fa fa-file-alt",
                    requiredPermissionName: RatTrackerPermissions.Schools.Default)
            );

            context.Menu.AddItem(
                new ApplicationMenuItem(
                    RatTrackerMenus.Results,
                    l["Menu:Results"],
                    url: "/Results",
                    icon: "fa fa-file-alt",
                    requiredPermissionName: RatTrackerPermissions.Results.Default)
            );
        }
    }
}
