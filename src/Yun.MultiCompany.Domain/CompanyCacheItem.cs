using System;
using Volo.Abp;
using Volo.Abp.MultiTenancy;

namespace Yun.MultiCompany
{
    [Serializable]
    [IgnoreMultiTenancy]
    public class CompanyCacheItem
    {
        private const string CacheKeyFormat = "i:{0},n:{1}";

        public CompanyConfiguration Value { get; set; }

        public CompanyCacheItem()
        {

        }

        public CompanyCacheItem(CompanyConfiguration value)
        {
            Value = value;
        }

        public static string CalculateCacheKey(Guid? id, string name)
        {
            if (id == null && name.IsNullOrWhiteSpace())
            {
                throw new AbpException("Both id and name can't be invalid.");
            }

            return string.Format(CacheKeyFormat,
                id?.ToString() ?? "null",
                (name.IsNullOrWhiteSpace() ? "null" : name));
        }
    }
}