using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;
using System.Linq;
using Volo.Abp.Identity;

namespace Yun.MultiCompany
{
    public class CompanyManage : DomainService, ICompanyManage
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyRoleRepository _companyRoleRepository;
        private readonly IRepository<CompanyUserRole> CompanyUserRoleRepository;
        private readonly IRepository<UserCompany> UserCompanyRepository;
        private readonly IRepository<IdentityUser, Guid> userRepository;


        public CompanyManage(
            ICompanyRepository companyRepository
            , ICompanyRoleRepository companyRoleRepository
            , IRepository<CompanyUserRole> companyUserRoleRepository
            , IRepository<UserCompany> userCompanyRepository
            , IRepository<IdentityUser, Guid> userRepository)
        {
            _companyRepository = companyRepository;
            _companyRoleRepository = companyRoleRepository;
            CompanyUserRoleRepository = companyUserRoleRepository;
            UserCompanyRepository = userCompanyRepository;
            this.userRepository = userRepository;
        }


        public async Task<Company> CreateAsync(string companyName, Guid userId)
        {

            Check.NotNull(companyName, nameof(companyName));

            await ValidateNameAsync(companyName);
            Company company = new Company(GuidGenerator.Create(), companyName);
            company.UserCompanys = new List<UserCompany>()
            {
                 new UserCompany(userId,company.Id)
            };

            //默认角色处理
            var role = await CreateAsync(new CompanyRole(GuidGenerator.Create(), company.Id, "管理员"));
            //用户加入默认角色
            var cur = await AddUserToRoleAsync(role, userId);
            company = await _companyRepository.InsertAsync(company);
            return company;
        }

        public virtual async Task ChangeNameAsync(Company company, string name)
        {
            Check.NotNull(company, nameof(company));
            Check.NotNull(name, nameof(name));

            await ValidateNameAsync(name, company.Id);
            company.SetName(name);
        }


        private async Task ValidateNameAsync(string name, Guid? expectedId = null)
        {
            var company = await _companyRepository.FindByNameAsync(name);
            if (company != null && company.Id != expectedId)
            {
                throw new UserFriendlyException("Duplicate company name: " + name); //TODO: A domain exception would be better..?
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyRole"></param>
        /// <returns></returns>
        public async Task<CompanyRole> CreateAsync(CompanyRole companyRole)
        {
            Check.NotNull(companyRole, nameof(companyRole));

            //检查角色名
            await ValidateRoleNameAsync(companyRole.RoleName);

            companyRole = await _companyRoleRepository.InsertAsync(companyRole);

            return companyRole;
        }

        public virtual async Task ChangeNameAsync(CompanyRole companyRole, string name)
        {
            Check.NotNull(companyRole, nameof(companyRole));
            Check.NotNull(name, nameof(name));

            await ValidateRoleNameAsync(name, companyRole.Id);

            companyRole.SetRoleName(name);
        }


        private async Task ValidateRoleNameAsync(string name, Guid? expectedId = null)
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<IdentityUser>> GetCompanyUsersAsync()
        {
            var cusers = await UserCompanyRepository.GetQueryableAsync();
            var users = await userRepository.GetQueryableAsync();

            var q = from user in users
                    join cu in cusers on user.Id equals cu.UserId
                    select user;

            return q.ToList();

        }
    }
}