using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yun.MultiCompany
{
    public interface ICompanyConfigurationProvider
    {
        Task<CompanyConfiguration> GetAsync(bool saveResolveResult = false);
    }
}
