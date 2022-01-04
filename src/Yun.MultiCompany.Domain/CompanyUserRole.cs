using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Yun.MultiCompany
{
    /// <summary>
    /// 公司用户岗位表
    /// </summary>
    public class CompanyUserRole : FullAuditedEntity, IMultiCompany
    {

        public virtual Company Company { get; set; }

        public virtual IdentityUser IdentityUser { get; set; }
        public virtual CompanyRole CompanyRole { get; set; }

        private CompanyUserRole()
        {

        }
        /// <summary>
        /// 岗位id
        /// </summary>
        public virtual Guid CompanyRoleId { get; private set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public virtual Guid UserId { get; private set; }
        /// <summary>
        /// 公司Id
        /// </summary>
        public virtual Guid CompanyId { get; private set; }


        public override object[] GetKeys()
        {
            return new object[] { CompanyRoleId, UserId, CompanyId };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="companyRoleId"></param>
        /// <exception cref="ArgumentNullException">当使用guid默认值传入的时候</exception>
        public CompanyUserRole(Guid userId, Guid companyRoleId, Guid companyId)
        {
            if (userId == default(Guid)) throw new ArgumentNullException(nameof(userId));
            if (companyRoleId == default(Guid)) throw new ArgumentNullException(nameof(companyRoleId));
            if (companyId == default(Guid)) throw new ArgumentNullException(nameof(companyId));
            UserId = userId;
            CompanyRoleId = companyRoleId;
            CompanyId = companyId;
        }
    }
}
