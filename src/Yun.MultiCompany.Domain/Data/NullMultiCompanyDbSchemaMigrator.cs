using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Yun.MultiCompany.Data
{
    /* This is used if database provider does't define
     * IMultiCompanyDbSchemaMigrator implementation.
     */
    public class NullMultiCompanyDbSchemaMigrator : IMultiCompanyDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}