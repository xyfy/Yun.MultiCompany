using JetBrains.Annotations;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Volo.Abp;

namespace Yun.MultiCompany
{
    public static class YunClaimsIdentityExtensions
    {
        public const string CompanyId = "companyid";
        public static Guid? FindCompanyId([NotNull] this ClaimsPrincipal principal)
        {
            Check.NotNull(principal, nameof(principal));

            var companyIdOrNull = principal.Claims?.FirstOrDefault(c => c.Type == CompanyId);
            if (companyIdOrNull == null || companyIdOrNull.Value.IsNullOrWhiteSpace())
            {
                return null;
            }

            if (Guid.TryParse(companyIdOrNull.Value, out var guid))
            {
                return guid;
            }

            return null;
        }

        public static Guid? FindCompanyId([NotNull] this IIdentity identity)
        {
            Check.NotNull(identity, nameof(identity));

            var claimsIdentity = identity as ClaimsIdentity;

            var companyIdOrNull = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == CompanyId);
            if (companyIdOrNull == null || companyIdOrNull.Value.IsNullOrWhiteSpace())
            {
                return null;
            }

            if (Guid.TryParse(companyIdOrNull.Value, out var guid))
            {
                return guid;
            }

            return null;
        }

    }
}
