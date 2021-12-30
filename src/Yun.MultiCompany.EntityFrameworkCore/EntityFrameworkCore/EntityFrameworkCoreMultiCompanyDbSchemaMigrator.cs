using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Yun.MultiCompany.Data;
using Volo.Abp.DependencyInjection;

namespace Yun.MultiCompany.EntityFrameworkCore
{
    public class EntityFrameworkCoreMultiCompanyDbSchemaMigrator
        : IMultiCompanyDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreMultiCompanyDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the MultiCompanyDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<MultiCompanyDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
