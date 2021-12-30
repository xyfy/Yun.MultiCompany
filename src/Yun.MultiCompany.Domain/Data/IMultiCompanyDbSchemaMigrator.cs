using System.Threading.Tasks;

namespace Yun.MultiCompany.Data
{
    public interface IMultiCompanyDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
