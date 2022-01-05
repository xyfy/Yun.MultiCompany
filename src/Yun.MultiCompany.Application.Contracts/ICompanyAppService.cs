using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Yun.MultiCompany
{
    public interface ICompanyAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CompanyUserDto>> GetCompanyUsersAsync();
    }

    public class CompanyUserDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public Guid CompanyId { get; set; }
    }
}
