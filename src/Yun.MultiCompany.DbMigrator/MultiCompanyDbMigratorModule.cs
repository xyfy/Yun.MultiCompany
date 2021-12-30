using Yun.MultiCompany.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Yun.MultiCompany.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(MultiCompanyEntityFrameworkCoreModule),
        typeof(MultiCompanyApplicationContractsModule)
        )]
    public class MultiCompanyDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
