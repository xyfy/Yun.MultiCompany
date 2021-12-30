using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Data;

namespace Yun.MultiCompany
{
    [Serializable]
    public class CompanyConfiguration
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public CompanyConfiguration()
        {
            IsActive = true;
        }

        public CompanyConfiguration(Guid id, [NotNull] string name)
            : this()
        {
            Check.NotNull(name, nameof(name));

            Id = id;
            Name = name;
        }
    }
}
