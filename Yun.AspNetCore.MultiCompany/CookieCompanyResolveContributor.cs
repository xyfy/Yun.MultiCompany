using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;
using Yun.MultiCompany;

namespace Yun.AspNetCore.MultiCompany
{
    public class CookieCompanyResolveContributor : HttpCompanyResolveContributorBase
    {
        public const string ContributorName = "Cookie";

        public override string Name => ContributorName;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override Task<string?> GetCompanyIdOrNameFromHttpContextOrNullAsync([NotNull] ICompanyResolveContext context, [NotNull] HttpContext httpContext)
        {
            return Task.FromResult(httpContext.Request.Cookies[context.GetYunAspNetCoreMultiCompanyOptions().CompanyKey]);
        }
    }
}
