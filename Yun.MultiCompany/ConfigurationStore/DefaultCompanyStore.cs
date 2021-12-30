using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Yun.MultiCompany.ConfigurationStore
{
    [Dependency(TryRegister = true)]
    public class DefaultCompanyStore : ICompanyStore, ITransientDependency
    {
        private readonly YunDefaultCompanyStoreOptions _options;

        public DefaultCompanyStore(IOptionsMonitor<YunDefaultCompanyStoreOptions> options)
        {
            _options = options.CurrentValue;
        }

        public Task<CompanyConfiguration> FindAsync(string name)
        {
            return Task.FromResult(Find(name));
        }

        public Task<CompanyConfiguration> FindAsync(Guid id)
        {
            return Task.FromResult(Find(id));
        }

        public CompanyConfiguration Find(string name)
        {
            return _options.Companies?.FirstOrDefault(t => t.Name == name);
        }

        public CompanyConfiguration Find(Guid id)
        {
            return _options.Companies?.FirstOrDefault(t => t.Id == id);
        }
    }
}
