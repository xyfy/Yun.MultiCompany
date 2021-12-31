using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace Yun.MultiCompany
{
    public class Company : FullAuditedAggregateRoot<Guid>
    {
        public virtual IEnumerable<UserCompany> UserCompanys { get; set; }


        public virtual IEnumerable<CompanyRole> Roles { get; set; }

        private Company()
        {
            UserCompanys = new List<UserCompany>();
            Roles = new List<CompanyRole>();
        }
        /// <summary>
        /// 公司名字
        /// </summary>
        [DynamicStringLength(typeof(MultiCompanyConsts), nameof(MultiCompanyConsts.MaxNameLength))]
        public virtual string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Company(Guid id, string name) : base(id)
        {
            if (id == default(Guid)) throw new ArgumentNullException(nameof(id));
            SetName(name);
        }

        public void SetName(string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Name = name;
        }

        [NotMapped]
        public virtual IEnumerable<IdentityUser> Users
        {
            get
            {
                return UserCompanys.Select(x => x.User).ToList();
            }
        }
    }
}
