using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Yun.MultiCompany
{

    public interface ICompanyRepository : IBasicRepository<Company, Guid>
    {
        Task<Company> FindByNameAsync(string name, bool includeDetails = true, CancellationToken cancellationToken = default);

        [Obsolete("Use FindByNameAsync method.")]
        Company FindByName(
            string name,
            bool includeDetails = true
        );

        [Obsolete("Use FindAsync method.")]
        Company FindById(
            Guid id,
            bool includeDetails = true
        );

        Task<List<Company>> GetListAsync(
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string filter = null,
            bool includeDetails = false,
            CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(
            string filter = null,
            CancellationToken cancellationToken = default);
    }
}
