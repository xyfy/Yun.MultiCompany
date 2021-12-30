using JetBrains.Annotations;
using System;
using Volo.Abp;

namespace Yun.MultiCompany
{
    public class BasicCompanyInfo
    {
        /// <summary>
        /// Null indicates the null srm.
        /// Not null value for a company.
        /// </summary>
        [CanBeNull]
        public Guid? CompanyId { get; }

        /// <summary>
        /// Name of the company if <see cref="CompanyId"/> is not null.
        /// </summary>
        [CanBeNull]
        public string Name { get; }

        public BasicCompanyInfo(Guid? companyId, string name = null)
        {
            CompanyId = companyId;
            Name = name;
        }
    }
}
