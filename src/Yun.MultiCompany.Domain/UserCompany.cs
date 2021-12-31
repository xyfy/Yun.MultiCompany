using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Yun.MultiCompany
{
    /// <summary>
    /// 公司用户表
    /// </summary>
    public class UserCompany : FullAuditedEntity, IMultiCompany
    {

        private UserCompany()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="companyId"></param>
        /// <exception cref="ArgumentNullException">当使用guid默认值传入的时候</exception>
        public UserCompany(Guid userId, Guid companyId)
        {
            if (userId == default(Guid)) throw new ArgumentNullException(nameof(userId));
            if (companyId == default(Guid)) throw new ArgumentNullException(nameof(companyId));
            UserId = userId;
            CompanyId = companyId;
        }

        public override object[] GetKeys()
        {
            return new object[] { CompanyId, UserId };
        }

        [ForeignKey(nameof(UserId))]
        public virtual IdentityUser User { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [ForeignKey(nameof(CompanyId))]
        public virtual Company Company { get; set; }

        /// <summary>
        /// 
        /// </summary>



        /// <summary>
        /// 公司id
        /// </summary>
        public virtual Guid CompanyId { get; private set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public virtual Guid UserId { get; private set; }
    }
}
