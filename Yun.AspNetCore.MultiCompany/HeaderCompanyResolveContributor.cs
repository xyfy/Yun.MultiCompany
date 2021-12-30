using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using Yun.MultiCompany;

namespace Yun.AspNetCore.MultiCompany
{
    public class HeaderCompanyResolveContributor : HttpCompanyResolveContributorBase
    {
        public const string ContributorName = "Header";

        public override string Name => ContributorName;

        protected override Task<string?> GetCompanyIdOrNameFromHttpContextOrNullAsync([NotNull] ICompanyResolveContext context, [NotNull] HttpContext httpContext)
        {
            if (httpContext.Request.Headers.IsNullOrEmpty())
            {
                return Task.FromResult((string?)null);
            }

            var companyIdKey = context.GetYunAspNetCoreMultiCompanyOptions().CompanyKey;

            var companyIdHeader = httpContext.Request.Headers[companyIdKey];
            if (companyIdHeader == string.Empty || companyIdHeader.Count < 1)
            {
                return Task.FromResult((string?)null);
            }

            if (companyIdHeader.Count > 1)
            {
                Log(context, $"HTTP request includes more than one {companyIdKey} header value. First one will be used. All of them: {companyIdHeader.JoinAsString(", ")}");
            }

            return Task.FromResult((string?)companyIdHeader.First());
        }

        protected virtual void Log(ICompanyResolveContext context, string text)
        {
            context
                .ServiceProvider
                .GetRequiredService<ILogger<HeaderCompanyResolveContributor>>()
                .LogWarning(text);
        }
    }
}
