using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace Yun.MultiCompany
{
    public class CompanyCacheItemInvalidator : ILocalEventHandler<EntityChangedEventData<Company>>, ITransientDependency
    {
        protected IDistributedCache<CompanyCacheItem> Cache { get; }

        public CompanyCacheItemInvalidator(IDistributedCache<CompanyCacheItem> cache)
        {
            Cache = cache;
        }

        public virtual async Task HandleEventAsync(EntityChangedEventData<Company> eventData)
        {
            await Cache.RemoveAsync(CompanyCacheItem.CalculateCacheKey(eventData.Entity.Id, null), considerUow: true);
            await Cache.RemoveAsync(CompanyCacheItem.CalculateCacheKey(null, eventData.Entity.Name), considerUow: true);
        }
    }
}