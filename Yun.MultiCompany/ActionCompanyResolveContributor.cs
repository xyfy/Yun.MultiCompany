using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp;

namespace Yun.MultiCompany
{


    public class ActionCompanyResolveContributor : CompanyResolveContributorBase
    {
        public const string ContributorName = "Action";

        public override string Name => ContributorName;

        private readonly Action<ICompanyResolveContext> _resolveAction;

        public ActionCompanyResolveContributor([NotNull] Action<ICompanyResolveContext> resolveAction)
        {
            Check.NotNull(resolveAction, nameof(resolveAction));

            _resolveAction = resolveAction;
        }

        public override Task ResolveAsync(ICompanyResolveContext context)
        {
            _resolveAction(context);
            return Task.CompletedTask;
        }
    }
}
