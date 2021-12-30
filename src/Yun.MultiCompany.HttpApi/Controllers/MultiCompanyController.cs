using Yun.MultiCompany.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Yun.MultiCompany.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class MultiCompanyController : AbpControllerBase
    {
        protected MultiCompanyController()
        {
            LocalizationResource = typeof(MultiCompanyResource);
        }
    }
}