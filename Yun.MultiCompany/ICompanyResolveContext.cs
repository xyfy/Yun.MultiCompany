using JetBrains.Annotations;
using Volo.Abp.DependencyInjection;

namespace Yun.MultiCompany
{
    public interface ICompanyResolveContext : IServiceProviderAccessor
    {
        [CanBeNull]
        string CompanyIdOrName { get; set; }

        bool Handled { get; set; }
    }
}
