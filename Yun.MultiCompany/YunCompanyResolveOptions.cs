using JetBrains.Annotations;
using System.Collections.Generic;

namespace Yun.MultiCompany
{
    public class YunCompanyResolveOptions
    {
        [NotNull]
        public List<ICompanyResolveContributor> CompanyResolvers { get; }

        public YunCompanyResolveOptions()
        {
            CompanyResolvers = new List<ICompanyResolveContributor>
            {
                //new CurrentUserCompanyResolveContributor ()
            };
        }
    }
}
