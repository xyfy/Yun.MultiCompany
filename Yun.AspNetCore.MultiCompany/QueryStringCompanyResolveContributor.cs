using Microsoft.AspNetCore.Http;
using Yun.MultiCompany;
using System.Diagnostics.CodeAnalysis;

namespace Yun.AspNetCore.MultiCompany
{
    public class QueryStringCompanyResolveContributor : HttpCompanyResolveContributorBase
    {
        public const string ContributorName = "QueryString";

        public override string Name => ContributorName;

        protected override Task<string?> GetCompanyIdOrNameFromHttpContextOrNullAsync([NotNull] ICompanyResolveContext context, [NotNull] HttpContext httpContext)
        {
            if (httpContext.Request.QueryString.HasValue)
            {
                var tenantKey = context.GetYunAspNetCoreMultiCompanyOptions().CompanyKey;
                if (httpContext.Request.Query.ContainsKey(tenantKey))
                {
                    var tenantValue = httpContext.Request.Query[tenantKey].ToString();
                    if (tenantValue.IsNullOrWhiteSpace())
                    {
                        context.Handled = true;
                        return Task.FromResult<string?>(null);
                    }

                    return Task.FromResult((string?)tenantValue);
                }
            }

            return Task.FromResult<string?>(null);
        }
    }
}
