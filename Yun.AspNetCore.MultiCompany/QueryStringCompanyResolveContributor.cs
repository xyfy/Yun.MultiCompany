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
                var companyKey = context.GetYunAspNetCoreMultiCompanyOptions().CompanyKey;
                if (httpContext.Request.Query.ContainsKey(companyKey))
                {
                    var companyValue = httpContext.Request.Query[companyKey].ToString();
                    if (companyValue.IsNullOrWhiteSpace())
                    {
                        context.Handled = true;
                        return Task.FromResult<string?>(null);
                    }

                    return Task.FromResult((string?)companyValue);
                }
            }

            return Task.FromResult<string?>(null);
        }
    }
}
