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
        private readonly ICompanyConfigurationProvider _tenantConfigurationProvider;
        private readonly ICurrentCompany _currentCompany;
        private readonly YunAspNetCoreMultiCompanyOptions _options;
        private readonly ICompanyResolveResultAccessor _tenantResolveResultAccessor;

        public MultiCompanyMiddleware(
            ICompanyConfigurationProvider tenantConfigurationProvider,
            ICurrentCompany currentCompany,
            IOptions<YunAspNetCoreMultiCompanyOptions> options,
            ICompanyResolveResultAccessor tenantResolveResultAccessor)
        {
            _tenantConfigurationProvider = tenantConfigurationProvider;
            _currentCompany = currentCompany;
            _tenantResolveResultAccessor = tenantResolveResultAccessor;
            _options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            CompanyConfiguration tenant;
            try
            {
                tenant = await _tenantConfigurationProvider.GetAsync(saveResolveResult: true);
            }
            catch (Exception e)
            {
                await _options.MultiCompanyMiddlewareErrorPageBuilder(context, e);
                return;
            }

            if (tenant?.Id != _currentCompany.Id)
            {
                using (_currentCompany.Change(tenant?.Id, tenant?.Name))
                {
                    if (_tenantResolveResultAccessor.Result != null &&
                        _tenantResolveResultAccessor.Result.AppliedResolvers.Contains(QueryStringCompanyResolveContributor.ContributorName))
                    {
                        AbpMultiCompanyCookieHelper.SetCompanyCookie(context, _currentCompany.Id, _options.CompanyKey);
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
