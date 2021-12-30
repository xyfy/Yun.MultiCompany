using System;

namespace Yun.MultiCompany
{
    public class CompanyResolveContext : ICompanyResolveContext
    {
        public IServiceProvider ServiceProvider { get; }

        public string CompanyIdOrName { get; set; }

        public bool Handled { get; set; }

        public bool HasResolvedCompanyOrHost()
        {
            return Handled || CompanyIdOrName != null;
        }

        public CompanyResolveContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
