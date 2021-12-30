using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Yun.MultiCompany;

namespace Yun.AspNetCore.MultiCompany
{
    public abstract class HttpCompanyResolveContributorBase : CompanyResolveContributorBase
    {
        public override async Task ResolveAsync(ICompanyResolveContext context)
        {
            var httpContext = context.GetHttpContext();
            if (httpContext == null)
            {
                return;
            }

            try
            {
                await ResolveFromHttpContextAsync(context, httpContext);
            }
            catch (Exception e)
            {
                context.ServiceProvider
                    .GetRequiredService<ILogger<HttpCompanyResolveContributorBase>>()
                    .LogWarning(e.ToString());
            }
        }

        protected virtual async Task ResolveFromHttpContextAsync(ICompanyResolveContext context, HttpContext httpContext)
        {
            var companyIdOrName = await GetCompanyIdOrNameFromHttpContextOrNullAsync(context, httpContext);
            if (!companyIdOrName.IsNullOrEmpty())
            {
                context.CompanyIdOrName = companyIdOrName;
            }
        }

        protected abstract Task<string?> GetCompanyIdOrNameFromHttpContextOrNullAsync([NotNull] ICompanyResolveContext context, [NotNull] HttpContext httpContext);
    }
}
