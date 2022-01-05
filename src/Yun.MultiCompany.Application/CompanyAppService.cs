using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Yun.MultiCompany
{
    public class CompanyAppService : MultiCompanyAppService, ICompanyAppService
    {
        public ICurrentCompany CurrentCompany => LazyServiceProvider.LazyGetRequiredService<ICurrentCompany>();
        protected virtual Guid? CurrentCompanyId => CurrentCompany?.Id;
        private readonly ICompanyManage _companyManage;

        public CompanyAppService(ICompanyManage companyManage)
        {
            _companyManage = companyManage;
        }

        public async Task<IEnumerable<CompanyUserDto>> GetCompanyUsersAsync()
        {
            var users = await _companyManage.GetCompanyUsersAsync();

            return users.Select(u => new CompanyUserDto
            {
                UserId = u.Id,
                UserName = u.UserName,
                CompanyId = CurrentCompanyId.Value
            });
        }
    }
}
