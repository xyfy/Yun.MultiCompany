using System.Collections.Generic;

namespace Yun.MultiCompany
{
    public class CompanyResolveResult
    {
        public string CompanyIdOrName { get; set; }

        public List<string> AppliedResolvers { get; }

        public CompanyResolveResult()
        {
            AppliedResolvers = new List<string>();
        }
    }
}
