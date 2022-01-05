using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Identity;

namespace Yun.MultiCompany
{
    public interface ICompanyManage : IDomainService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Company> CreateAsync(string companyName, Guid userId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task ChangeNameAsync(Company company, string name);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<IdentityUser>> GetCompanyUsersAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<CompanyRole> CreateAsync(CompanyRole role);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyRole"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task ChangeNameAsync(CompanyRole companyRole, string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <param name="userId">用户ID，必传</param>
        /// <returns></returns>
        Task<CompanyUserRole> AddUserToRoleAsync(CompanyRole role, Guid? userId);
    }
}
