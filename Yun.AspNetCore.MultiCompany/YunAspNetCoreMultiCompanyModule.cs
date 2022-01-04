using Volo.Abp.AspNetCore;
using Volo.Abp.Modularity;
using Yun.MultiCompany;

namespace Yun.AspNetCore.MultiCompany
{
    [DependsOn(
    typeof(YunMultiCompanyModule),
    typeof(AbpAspNetCoreModule)
    )]

    public class YunAspNetCoreMultiCompanyModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<YunCompanyResolveOptions>(options =>
            {
                options.CompanyResolvers.Add(new QueryStringCompanyResolveContributor());
                //options.CompanyResolvers.Add(new FormCompanyResolveContributor());
                //options.CompanyResolvers.Add(new RouteCompanyResolveContributor());
                options.CompanyResolvers.Add(new HeaderCompanyResolveContributor());
                options.CompanyResolvers.Add(new CookieCompanyResolveContributor());
            });
        }
    }
}