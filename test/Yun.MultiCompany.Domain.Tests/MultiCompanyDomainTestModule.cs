using Yun.MultiCompany.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Yun.MultiCompany
{
    [DependsOn(
        typeof(MultiCompanyEntityFrameworkCoreTestModule)
        )]
    public class MultiCompanyDomainTestModule : AbpModule
    {

    }
}