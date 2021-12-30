using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Yun.MultiCompany
{
    public class CompanyConfigurationProvider : ICompanyConfigurationProvider, ITransientDependency
    {
        protected virtual ICompanyResolver CompanyResolver { get; }
        protected virtual ICompanyStore CompanyStore { get; }
        protected virtual ICompanyResolveResultAccessor CompanyResolveResultAccessor { get; }

        public CompanyConfigurationProvider(
            ICompanyResolver companyResolver,
            ICompanyStore companyStore,
            ICompanyResolveResultAccessor companyResolveResultAccessor)
        {
            CompanyResolver = companyResolver;
            CompanyStore = companyStore;
            CompanyResolveResultAccessor = companyResolveResultAccessor;
        }

        public virtual async Task<CompanyConfiguration> GetAsync(bool saveResolveResult = false)
        {
            var resolveResult = await CompanyResolver.ResolveCompanyIdOrNameAsync();

            if (saveResolveResult)
            {
                CompanyResolveResultAccessor.Result = resolveResult;
            }

            CompanyConfiguration company = null;
            if (resolveResult.CompanyIdOrName != null)
            {
                company = await FindCompanyAsync(resolveResult.CompanyIdOrName);

                if (company == null)
                {
                    throw new BusinessException(
                        code: "Yun.MultiCompany:010001",
                        message: "Company not found!",
                        details: "There is no company with the company id or name: " + resolveResult.CompanyIdOrName
                    );
                }

                if (!company.IsActive)
                {
                    throw new BusinessException(
                        code: "Yun.MultiCompany:010002",
                        message: "Company not active!",
                        details: "The company is no active with the company id or name: " + resolveResult.CompanyIdOrName
                    );
                }
            }

            return company;
        }

        protected virtual async Task<CompanyConfiguration> FindCompanyAsync(string companyIdOrName)
        {
            if (Guid.TryParse(companyIdOrName, out var parsedCompanyId))
            {
                return await CompanyStore.FindAsync(parsedCompanyId);
            }
            else
            {
                return await CompanyStore.FindAsync(companyIdOrName);
            }
        }
    }
}
