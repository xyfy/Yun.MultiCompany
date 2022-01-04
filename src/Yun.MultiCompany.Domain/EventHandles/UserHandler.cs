using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Users;

namespace Yun.MultiCompany.EventHandles
{
    public class UserHandler :
        IDistributedEventHandler<EntityCreatedEto<UserEto>>,
        IDistributedEventHandler<EntityDeletedEto<UserEto>>,
        IDistributedEventHandler<EntityUpdatedEto<UserEto>>,
        ITransientDependency
    {

        private ICompanyManage CompanyManage { get; }

        public UserHandler(ICompanyManage companyManage)
        {
            CompanyManage = companyManage;
        }

        public async Task HandleEventAsync(EntityCreatedEto<UserEto> eventData)
        {
            //TODO 用户新建，需要新建默认公司
            if (eventData.Entity.TenantId == null)
            {
                //宿主用户才创建公司
                await CompanyManage.CreateAsync("个人用户", eventData.Entity.Id);
            }
        }

        public async Task HandleEventAsync(EntityUpdatedEto<UserEto> eventData)
        {
            await Task.CompletedTask;
        }

        public async Task HandleEventAsync(EntityDeletedEto<UserEto> eventData)
        {
            await Task.CompletedTask;
        }
    }
}
