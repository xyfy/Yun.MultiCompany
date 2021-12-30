using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Data;
using Volo.Abp.EventBus.Abstractions;
using Volo.Abp.Modularity;
using Volo.Abp.Security;
using Yun.MultiCompany.ConfigurationStore;

namespace Yun.MultiCompany
{
    [DependsOn(
        typeof(AbpDataModule),
        typeof(AbpSecurityModule),
        typeof(AbpEventBusAbstractionsModule))]
    public partial class YunMultiCompanyModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<ICurrentCompanyAccessor>(AsyncLocalCurrentCompanyAccessor.Instance);

            var configuration = context.Services.GetConfiguration();
            Configure<YunDefaultCompanyStoreOptions>(configuration);
        }
    }
}
