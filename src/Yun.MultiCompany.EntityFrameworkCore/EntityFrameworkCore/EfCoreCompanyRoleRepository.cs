using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Yun.MultiCompany.EntityFrameworkCore
{
    public class EfCoreCompanyRoleRepository : EfCoreRepository<MultiCompanyDbContext, CompanyRole, Guid>, ICompanyRoleRepository
    {
        public EfCoreCompanyRoleRepository(IDbContextProvider<MultiCompanyDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<CompanyRole> FindByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .OrderBy(t => t.Id)
                .FirstOrDefaultAsync(t => t.RoleName == name, GetCancellationToken(cancellationToken));
        }
    }
}
