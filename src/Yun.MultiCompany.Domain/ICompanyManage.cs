using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

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
    }
}
