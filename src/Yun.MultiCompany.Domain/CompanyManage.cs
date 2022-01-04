using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;

namespace Yun.MultiCompany
{
    public class CompanyManage : DomainService, ICompanyManage
    {
        private ICompanyRoleManage CompanyRoleManage { get; }
        private ICompanyRepository _companyRepository;
        private IRepository<CompanyRole, Guid> _companyRoleRepository;
        private IRepository<UserCompany> _userCompanyRepository;
        private readonly IGuidGenerator _guidGenerator;


        public CompanyManage(IRepository<CompanyRole, Guid> companyRoleRepository
            , IRepository<UserCompany> userCompanyRepository
            , IGuidGenerator guidGenerator, ICompanyRepository companyRepository
            , ICompanyRoleManage companyRoleManage)
        {
            _companyRoleRepository = companyRoleRepository;
            _userCompanyRepository = userCompanyRepository;
            _guidGenerator = guidGenerator;
            _companyRepository = companyRepository;
            CompanyRoleManage = companyRoleManage;
        }


        public async Task<Company> CreateAsync(string companyName, Guid userId)
        {

            Check.NotNull(companyName, nameof(companyName));

            await ValidateNameAsync(companyName);
            Company company = new Company(_guidGenerator.Create(), companyName);
            company.UserCompanys = new List<UserCompany>()
            {
                 new UserCompany(userId,company.Id)
            };

            //默认角色处理
            var role = await CompanyRoleManage.CreateAsync(new CompanyRole(GuidGenerator.Create(), company.Id, "管理员"));
            //用户加入默认角色
            var cur = await CompanyRoleManage.AddUserToRoleAsync(role, userId);
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
    }
}
