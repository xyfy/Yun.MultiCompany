using System;
using System.Collections.Generic;
using System.Text;
using Yun.MultiCompany.Localization;
using Volo.Abp.Application.Services;

namespace Yun.MultiCompany
{
    /* Inherit your application services from this class.
     */
    public abstract class MultiCompanyAppService : ApplicationService
    {
        protected MultiCompanyAppService()
        {
            LocalizationResource = typeof(MultiCompanyResource);
        }
    }
}
