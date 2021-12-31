using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Validation;

namespace Yun.MultiCompany
{
    /// <summary>
    /// 公司岗位
    /// </summary>
    public class CompanyRole : FullAuditedAggregateRoot<Guid>, IMultiCompany
    {
        [DynamicStringLength(typeof(MultiCompanyConsts), nameof(MultiCompanyConsts.MaxRoleNameLength))]
        public virtual string RoleName { get; set; }
        private CompanyRole()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companyId"></param>
        /// <exception cref="ArgumentNullException">当使用guid默认值传入的时候</exception>
        protected CompanyRole(Guid id, Guid companyId) : base(id)
        {
            if (id == default(Guid)) throw new ArgumentNullException(nameof(id));
            if (id == default(Guid)) throw new ArgumentNullException(nameof(id));
            Id = id;
            CompanyId = companyId;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">主键id</param>
        /// <param name="companyId">公司id</param>
        /// <param name="roleName">角色名称</param>
        /// <remarks>
        /// guid传入默认值||角色名为空 会抛出<seealso cref="ArgumentNullException"/>
        /// </remarks>
        /// <exception cref="ArgumentNullException"></exception>
        public CompanyRole(Guid id, Guid companyId, string roleName) : this(id, companyId)
        {
            SetRoleName(roleName);
        }

        /// <summary>
        /// 设置角色名
        /// </summary>
        /// <param name="roleName"></param>
        public void SetRoleName(string roleName)
        {
            Check.NotNullOrWhiteSpace(roleName, nameof(roleName));
            //TODO 此处应当抛出事件，来同步角色以及权限的修改
            RoleName = roleName;
        }

        public virtual Guid CompanyId { get; set; }
    }
}
