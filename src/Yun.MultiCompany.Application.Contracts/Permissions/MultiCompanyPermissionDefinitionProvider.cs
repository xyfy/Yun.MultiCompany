using Yun.MultiCompany.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Yun.MultiCompany.Permissions
{
    public class MultiCompanyPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(MultiCompanyPermissions.GroupName);
            //Define your own permissions here. Example:
            //myGroup.AddPermission(MultiCompanyPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<MultiCompanyResource>(name);
        }
    }
}
