using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Yun.MultiCompany
{
    public interface ICompanyRoleRepository : IBasicRepository<CompanyRole, Guid>
    {
        Task<CompanyRole> FindByNameAsync(string name,
            CancellationToken cancellationToken = default);
    }
}
