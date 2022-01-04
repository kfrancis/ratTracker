using RatTracker.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace RatTracker.Permissions
{
    public class RatTrackerPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var myGroup = context.AddGroup(RatTrackerPermissions.GroupName);

            myGroup.AddPermission(RatTrackerPermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
            myGroup.AddPermission(RatTrackerPermissions.Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(RatTrackerPermissions.MyPermission1, L("Permission:MyPermission1"));

            var schoolPermission = myGroup.AddPermission(RatTrackerPermissions.SchoolPermissions.Default, L("Permission:Schools"));
            schoolPermission.AddChild(RatTrackerPermissions.SchoolPermissions.Create, L("Permission:Create"));
            schoolPermission.AddChild(RatTrackerPermissions.SchoolPermissions.Edit, L("Permission:Edit"));
            schoolPermission.AddChild(RatTrackerPermissions.SchoolPermissions.Delete, L("Permission:Delete"));

            var resultPermission = myGroup.AddPermission(RatTrackerPermissions.ResultPermissionss.Default, L("Permission:Results"));
            resultPermission.AddChild(RatTrackerPermissions.ResultPermissionss.Create, L("Permission:Create"));
            resultPermission.AddChild(RatTrackerPermissions.ResultPermissionss.Edit, L("Permission:Edit"));
            resultPermission.AddChild(RatTrackerPermissions.ResultPermissionss.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<RatTrackerResource>(name);
        }
    }
}