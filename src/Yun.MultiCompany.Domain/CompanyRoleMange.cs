using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Yun.MultiCompany
{
    public class CompanyRoleMange : DomainService, ICompanyRoleManage
    {
        private ICompanyRoleRepository _companyRoleRepository;
        private IRepository<CompanyUserRole> CompanyUserRoleRepository;
        public CompanyRoleMange(ICompanyRoleRepository companyRoleRepository, IRepository<CompanyUserRole> companyUserRoleRepository)
        {
            _companyRoleRepository = companyRoleRepository;
            CompanyUserRoleRepository = companyUserRoleRepository;
        }

        public Task<CompanyRole> CreateAsync(CompanyRole role)
        {

            throw new NotImplementedException();
        }



        public virtual async Task ChangeNameAsync(CompanyRole companyRole, string name)
        {
            Check.NotNull(companyRole, nameof(companyRole));
            Check.NotNull(name, nameof(name));

            await ValidateNameAsync(name, companyRole.Id);

            companyRole.SetRoleName(name);
        }


        private async Task ValidateNameAsync(string name, Guid? expectedId = null)
        {
            var company = await _companyRoleRepository.FindByNameAsync(name);
            if (company != null && company.Id != expectedId)
            {
                throw new UserFriendlyException("Duplicate company role name: " + name); //TODO: A domain exception would be better..?
            }
        }

        public async Task<CompanyUserRole> AddUserToRoleAsync(CompanyRole companyRole, Guid? userId)
        {
            Check.NotNull(companyRole, nameof(companyRole));
            Check.NotNull(userId, nameof(userId));

            var cur = await CompanyUserRoleRepository.FindAsync(cur => cur.UserId == userId && cur.CompanyRoleId == companyRole.Id);
            if (cur == null)
            {
                //用户不存在指定角色
                cur = new CompanyUserRole(userId.Value, companyRole.Id, companyRole.CompanyId);
                cur = await CompanyUserRoleRepository.InsertAsync(cur);
            }
            return cur;
        }
    }
}
