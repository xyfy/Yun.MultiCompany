using System;
using System.Text;
using System.Threading.Tasks;

namespace Yun.MultiCompany
{
    public interface ICompanyStore
    {
        Task<CompanyConfiguration> FindAsync(string name);

        Task<CompanyConfiguration> FindAsync(Guid id);

        [Obsolete("Use FindAsync method.")]
        CompanyConfiguration Find(string name);

        [Obsolete("Use FindAsync method.")]
        CompanyConfiguration Find(Guid id);
    }
}
