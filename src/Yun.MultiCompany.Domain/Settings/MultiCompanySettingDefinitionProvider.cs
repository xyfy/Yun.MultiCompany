using Volo.Abp.Settings;

namespace Yun.MultiCompany.Settings
{
    public class MultiCompanySettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(MultiCompanySettings.MySetting1));
        }
    }
}
