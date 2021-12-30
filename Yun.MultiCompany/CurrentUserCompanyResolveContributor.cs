using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Volo.Abp.Users;
using System;
namespace Yun.MultiCompany
{
    public class CurrentUserCompanyResolveContributor : CompanyResolveContributorBase
    {
        public const string ContributorName = "CurrentUser";

        public override string Name => ContributorName;

        public override Task ResolveAsync(ICompanyResolveContext context)
        {
            var currentUser = context.ServiceProvider.GetRequiredService<CurrentCompanyUser>();
            if (currentUser.IsAuthenticated)
            {
                context.Handled = true;
                context.CompanyIdOrName = currentUser.CompanyId?.ToString();
            }

            return Task.CompletedTask;
        }
    }
}
