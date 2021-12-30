using System.Threading.Tasks;

namespace Yun.MultiCompany
{
    public interface ICompanyResolveContributor
    {
        string Name { get; }

        Task ResolveAsync(ICompanyResolveContext context);
    }
}
