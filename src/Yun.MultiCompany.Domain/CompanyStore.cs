using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;

namespace Yun.MultiCompany
{
    public class CompanyStore : ICompanyStore, ITransientDependency
    {
        protected ICompanyRepository CompanyRepository { get; }
        protected IObjectMapper<MultiCompanyDomainModule> ObjectMapper { get; }
        protected ICurrentCompany CurrentCompany { get; }
        protected IDistributedCache<CompanyCacheItem> Cache { get; }

        public CompanyStore(
            ICompanyRepository companyRepository,
            IObjectMapper<MultiCompanyDomainModule> objectMapper,
            ICurrentCompany currentCompany,
            IDistributedCache<CompanyCacheItem> cache)
        {
            CompanyRepository = companyRepository;
            ObjectMapper = objectMapper;
            CurrentCompany = currentCompany;
            Cache = cache;
        }

        public virtual async Task<CompanyConfiguration> FindAsync(string name)
        {
            return (await GetCacheItemAsync(null, name)).Value;
        }

        public virtual async Task<CompanyConfiguration> FindAsync(Guid id)
        {
            return (await GetCacheItemAsync(id, null)).Value;
        }

        [Obsolete("Use FindAsync method.")]
        public virtual CompanyConfiguration Find(string name)
        {
            return (GetCacheItem(null, name)).Value;
        }

        [Obsolete("Use FindAsync method.")]
        public virtual CompanyConfiguration Find(Guid id)
        {
            return (GetCacheItem(id, null)).Value;
        }

        protected virtual async Task<CompanyCacheItem> GetCacheItemAsync(Guid? id, string name)
        {
            var cacheKey = CalculateCacheKey(id, name);

            var cacheItem = await Cache.GetAsync(cacheKey, considerUow: true);
            if (cacheItem != null)
            {
                return cacheItem;
            }

            if (id.HasValue)
            {
                using (CurrentCompany.Change(null)) //TODO: No need this if we can implement to define host side (or company-independent) entities!
                {
                    var company = await CompanyRepository.FindAsync(id.Value);
                    return await SetCacheAsync(cacheKey, company);
                }
            }

            if (!name.IsNullOrWhiteSpace())
            {
                using (CurrentCompany.Change(null)) //TODO: No need this if we can implement to define host side (or company-independent) entities!
                {
                    var company = await CompanyRepository.FindByNameAsync(name);
                    return await SetCacheAsync(cacheKey, company);
                }
            }

            throw new AbpException("Both id and name can't be invalid.");
        }

        protected virtual async Task<CompanyCacheItem> SetCacheAsync(string cacheKey, [CanBeNull] Company company)
        {
            var companyConfiguration = company != null ? ObjectMapper.Map<Company, CompanyConfiguration>(company) : null;
            var cacheItem = new CompanyCacheItem(companyConfiguration);
            await Cache.SetAsync(cacheKey, cacheItem, considerUow: true);
            return cacheItem;
        }

        [Obsolete("Use GetCacheItemAsync method.")]
        protected virtual CompanyCacheItem GetCacheItem(Guid? id, string name)
        {
            var cacheKey = CalculateCacheKey(id, name);

            var cacheItem = Cache.Get(cacheKey, considerUow: true);
            if (cacheItem != null)
            {
                return cacheItem;
            }

            if (id.HasValue)
            {
                using (CurrentCompany.Change(null)) //TODO: No need this if we can implement to define host side (or company-independent) entities!
                {
                    var company = CompanyRepository.FindById(id.Value);
                    return SetCache(cacheKey, company);
                }
            }

            if (!name.IsNullOrWhiteSpace())
            {
                using (CurrentCompany.Change(null)) //TODO: No need this if we can implement to define host side (or company-independent) entities!
                {
                    var company = CompanyRepository.FindByName(name);
                    return SetCache(cacheKey, company);
                }
            }

            throw new AbpException("Both id and name can't be invalid.");
        }

        [Obsolete("Use SetCacheAsync method.")]
        protected virtual CompanyCacheItem SetCache(string cacheKey, [CanBeNull] Company company)
        {
            var companyConfiguration = company != null ? ObjectMapper.Map<Company, CompanyConfiguration>(company) : null;
            var cacheItem = new CompanyCacheItem(companyConfiguration);
            Cache.Set(cacheKey, cacheItem, considerUow: true);
            return cacheItem;
        }

        protected virtual string CalculateCacheKey(Guid? id, string name)
        {
            return CompanyCacheItem.CalculateCacheKey(id, name);
        }
    }
}