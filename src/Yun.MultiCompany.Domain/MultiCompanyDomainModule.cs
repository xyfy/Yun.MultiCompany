using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Yun.MultiCompany.MultiTenancy;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Emailing;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.IdentityServer;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.AutoMapper;
using Volo.Abp.Caching;
using Yun.AspNetCore.MultiCompany;

namespace Yun.MultiCompany
{
    [DependsOn(typeof(AbpAuditLoggingDomainModule),
        typeof(AbpBackgroundJobsDomainModule),
        typeof(AbpFeatureManagementDomainModule),
        typeof(AbpIdentityDomainModule),
        typeof(AbpPermissionManagementDomainIdentityModule),
        typeof(AbpIdentityServerDomainModule),
        typeof(AbpPermissionManagementDomainIdentityServerModule),
        typeof(AbpSettingManagementDomainModule),
        typeof(AbpTenantManagementDomainModule),
        typeof(Yun.AspNetCore.MultiCompany.YunAspNetCoreMultiCompanyModule),
        typeof(AbpEmailingModule)
    )]
    [DependsOn(typeof(MultiCompanyDomainSharedModule))]
    [DependsOn(typeof(YunMultiCompanyModule))]
    [DependsOn(typeof(AbpAutoMapperModule))]
    [DependsOn(typeof(AbpCachingModule))]
    public class MultiCompanyDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<MultiCompanyDomainModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<MultiCompanyDomainMappingProfile>(validate: true);
            });
            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = MultiTenancyConsts.IsEnabled;
            });

#if DEBUG
            context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
#endif

            Configure<AbpDistributedEntityEventOptions>(options =>
            {
                options.EtoMappings.Add<Company, CompanyEto>();
            });
        }
    }
}
