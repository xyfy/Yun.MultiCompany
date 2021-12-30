using Volo.Abp.DependencyInjection;

namespace Yun.MultiCompany
{
    public class NullCompanyResolveResultAccessor : ICompanyResolveResultAccessor, ISingletonDependency
    {
        public CompanyResolveResult Result
        {
            get => null;
            set { }
        }
    }
}
