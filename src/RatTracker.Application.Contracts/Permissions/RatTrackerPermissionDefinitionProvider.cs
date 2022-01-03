using RatTracker.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace RatTracker.Permissions
{
    public class RatTrackerPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(RatTrackerPermissions.GroupName);
            //Define your own permissions here. Example:
            //myGroup.AddPermission(RatTrackerPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<RatTrackerResource>(name);
        }
    }
}
