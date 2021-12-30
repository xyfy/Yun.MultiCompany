using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Yun.MultiCompany;

namespace Yun.AspNetCore.MultiCompany
{
    public static class CompanyResolveContextExtensions
    {
        public static YunAspNetCoreMultiCompanyOptions GetYunAspNetCoreMultiCompanyOptions(this ICompanyResolveContext context)
        {
            return context.ServiceProvider.GetRequiredService<IOptions<YunAspNetCoreMultiCompanyOptions>>().Value;
        }
    }
}
