using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Yun.AspNetCore.MultiCompany;

namespace Microsoft.AspNetCore.Builder
{
    public static class YunAspNetCoreMultiCompanyApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMultiCompany(this IApplicationBuilder app)
        {
            return app
                .UseMiddleware<MultiCompanyMiddleware>();
        }
    }
}
