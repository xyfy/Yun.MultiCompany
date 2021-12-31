using System;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Security.Claims;
using Volo.Abp.Users;

namespace Yun.MultiCompany
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(ICurrentUser), typeof(CurrentUser), typeof(CurrentCompanyUser))]
    public class CurrentCompanyUser : CurrentUser
    {
        private readonly ICurrentPrincipalAccessor _principalAccessor;

        public CurrentCompanyUser(ICurrentPrincipalAccessor principalAccessor) : base(principalAccessor)
        {
            _principalAccessor = principalAccessor;
        }

        /// <summary>
        /// 用户现在切换到的公司id
        /// </summary>
        public virtual Guid? CompanyId => _principalAccessor.Principal?.FindCompanyId();
    }
}
