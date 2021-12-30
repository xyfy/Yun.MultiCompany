using Volo.Abp.Modularity;

namespace Yun.MultiCompany
{
    [DependsOn(
        typeof(MultiCompanyApplicationModule),
        typeof(MultiCompanyDomainTestModule)
        )]
    public class MultiCompanyApplicationTestModule : AbpModule
    {

    }
}