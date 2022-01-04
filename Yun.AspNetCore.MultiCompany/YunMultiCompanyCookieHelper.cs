using Microsoft.AspNetCore.Http;

namespace Yun.AspNetCore.MultiCompany
{
    public static class YunMultiCompanyCookieHelper
    {
        public static void SetCompanyCookie(
            HttpContext context,
            Guid? companyId,
            string companyKey)
        {
            if (companyId != null)
            {
                context.Response.Cookies.Append(
                    companyKey,
                    companyId.Value.ToString(),
                    new CookieOptions
                    {
                        Path = "/",
                        HttpOnly = false,
                        IsEssential = true,
                        Expires = DateTimeOffset.Now.AddYears(10)
                    }
                );
            }
            else
            {
                context.Response.Cookies.Delete(companyKey);
            }
        }
    }
}
