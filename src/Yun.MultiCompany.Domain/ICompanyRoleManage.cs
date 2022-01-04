using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Yun.MultiCompany
{
    public interface ICompanyRoleManage : IDomainService
    {
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
