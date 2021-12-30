using Yun.MultiCompany.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Yun.MultiCompany.Web.Pages
{
    public abstract class MultiCompanyPageModel : AbpPageModel
    {
        protected MultiCompanyPageModel()
        {
            LocalizationResourceType = typeof(MultiCompanyResource);
        }
    }
}