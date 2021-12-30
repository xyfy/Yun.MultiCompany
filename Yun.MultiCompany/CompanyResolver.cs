using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Yun.MultiCompany
{
    public class CompanyResolver : ICompanyResolver, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly YunCompanyResolveOptions _options;

        public CompanyResolver(IOptions<YunCompanyResolveOptions> options, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _options = options.Value;
        }

        public virtual async Task<CompanyResolveResult> ResolveCompanyIdOrNameAsync()
        {
            var result = new CompanyResolveResult();

            using (var serviceScope = _serviceProvider.CreateScope())
            {
                var context = new CompanyResolveContext(serviceScope.ServiceProvider);

                foreach (var companyResolver in _options.CompanyResolvers)
                {
                    await companyResolver.ResolveAsync(context);

                    result.AppliedResolvers.Add(companyResolver.Name);

                    if (context.HasResolvedCompanyOrHost())
                    {
                        result.CompanyIdOrName = context.CompanyIdOrName;
                        break;
                    }
                }
            }

            return result;
        }
    }
}
