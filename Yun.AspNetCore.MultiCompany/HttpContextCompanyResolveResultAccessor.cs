using Microsoft.AspNetCore.Http;
using Volo.Abp.DependencyInjection;
using Yun.MultiCompany;

namespace Yun.AspNetCore.MultiCompany
{
    [Dependency(ReplaceServices = true)]
    public class HttpContextCompanyResolveResultAccessor : ICompanyResolveResultAccessor, ITransientDependency
    {
        public const string HttpContextItemName = "__AbpCompanyResolveResult";

        public CompanyResolveResult? Result
        {
            get => _httpContextAccessor.HttpContext?.Items[HttpContextItemName] as CompanyResolveResult;
            set
            {
                if (_httpContextAccessor.HttpContext == null)
                {
                    return;
                }

                _httpContextAccessor.HttpContext.Items[HttpContextItemName] = value;
            }
        }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextCompanyResolveResultAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }

    
}
