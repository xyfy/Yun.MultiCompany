using System.Threading.Tasks;

namespace Yun.MultiCompany
{
    public abstract class CompanyResolveContributorBase : ICompanyResolveContributor
    {
        public abstract string Name { get; }

        public abstract Task ResolveAsync(ICompanyResolveContext context);
    }
    
}
