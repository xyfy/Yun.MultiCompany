using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Yun.MultiCompany
{
    [Dependency(ReplaceServices = true)]
    public class MultiCompanyBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "MultiCompany";
    }
}
