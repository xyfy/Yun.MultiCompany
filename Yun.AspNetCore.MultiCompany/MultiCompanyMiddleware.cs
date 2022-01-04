using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.RequestLocalization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Localization;
using Yun.MultiCompany;
using Volo.Abp.Settings;

namespace Yun.AspNetCore.MultiCompany
{
    public class MultiCompanyMiddleware : IMiddleware, ITransientDependency
    {
        private readonly ICompanyConfigurationProvider _companyConfigurationProvider;
        private readonly ICurrentCompany _currentCompany;
        private readonly YunAspNetCoreMultiCompanyOptions _options;
        private readonly ICompanyResolveResultAccessor _companyResolveResultAccessor;

        public MultiCompanyMiddleware(
            ICompanyConfigurationProvider companyConfigurationProvider,
            ICurrentCompany currentCompany,
            IOptions<YunAspNetCoreMultiCompanyOptions> options,
            ICompanyResolveResultAccessor companyResolveResultAccessor)
        {
            _companyConfigurationProvider = companyConfigurationProvider;
            _currentCompany = currentCompany;
            _companyResolveResultAccessor = companyResolveResultAccessor;
            _options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            CompanyConfiguration company;
            try
            {
                company = await _companyConfigurationProvider.GetAsync(saveResolveResult: true);
            }
            catch (Exception e)
            {
                await _options.MultiCompanyMiddlewareErrorPageBuilder(context, e);
                return;
            }

            if (company?.Id != _currentCompany.Id)
            {
                using (_currentCompany.Change(company?.Id, company?.Name))
                {
                    if (_companyResolveResultAccessor.Result != null &&
                        _companyResolveResultAccessor.Result.AppliedResolvers.Contains(QueryStringCompanyResolveContributor.ContributorName))
                    {
                        YunMultiCompanyCookieHelper.SetCompanyCookie(context, _currentCompany.Id, _options.CompanyKey);
                    }

                    var requestCulture = await TryGetRequestCultureAsync(context);
                    if (requestCulture != null)
                    {
                        CultureInfo.CurrentCulture = requestCulture.Culture;
                        CultureInfo.CurrentUICulture = requestCulture.UICulture;
                        AbpRequestCultureCookieHelper.SetCultureCookie(
                            context,
                            requestCulture
                        );
                        context.Items[AbpRequestLocalizationMiddleware.HttpContextItemName] = true;
                    }

                    await next(context);
                }
            }
            else
            {
                await next(context);
            }
        }

        private async Task<RequestCulture?> TryGetRequestCultureAsync(HttpContext httpContext)
        {
            var requestCultureFeature = httpContext.Features.Get<IRequestCultureFeature>();

            /* If requestCultureFeature == null, that means the RequestLocalizationMiddleware was not used
             * and we don't want to set the culture. */
            if (requestCultureFeature == null)
            {
                return null;
            }

            /* If requestCultureFeature.Provider is not null, that means RequestLocalizationMiddleware
             * already picked a language, so we don't need to set the default. */
            if (requestCultureFeature.Provider != null)
            {
                return null;
            }

            var settingProvider = httpContext.RequestServices.GetRequiredService<ISettingProvider>();
            var defaultLanguage = await settingProvider.GetOrNullAsync(LocalizationSettingNames.DefaultLanguage);
            if (defaultLanguage.IsNullOrWhiteSpace())
            {
                return null;
            }

            string culture;
            string uiCulture;

            if (defaultLanguage.Contains(';'))
            {
                var splitted = defaultLanguage.Split(';');
                culture = splitted[0];
                uiCulture = splitted[1];
            }
            else
            {
                culture = defaultLanguage;
                uiCulture = defaultLanguage;
            }

            return new RequestCulture(culture, uiCulture);
        }
    }
}
