using Volo.Abp.Settings;

namespace RatTracker.Settings
{
    public class RatTrackerSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(RatTrackerSettings.MySetting1));
        }
    }
}
