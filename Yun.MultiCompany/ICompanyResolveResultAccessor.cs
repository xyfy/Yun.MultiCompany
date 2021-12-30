using JetBrains.Annotations;

namespace Yun.MultiCompany
{
    public interface ICompanyResolveResultAccessor
    {
        [CanBeNull]
        CompanyResolveResult Result { get; set; }
    }
}
