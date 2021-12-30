using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Yun.MultiCompany.Web.Pages
{
    public class IndexModel : MultiCompanyPageModel
    {
        public void OnGet()
        {
            
        }

        public async Task OnPostLoginAsync()
        {
            await HttpContext.ChallengeAsync("oidc");
        }
    }
}