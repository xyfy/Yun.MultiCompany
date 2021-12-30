using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Yun.MultiCompany.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class MultiCompanyDbContextFactory : IDesignTimeDbContextFactory<MultiCompanyDbContext>
    {
        public MultiCompanyDbContext CreateDbContext(string[] args)
        {
            MultiCompanyEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<MultiCompanyDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new MultiCompanyDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Yun.MultiCompany.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
