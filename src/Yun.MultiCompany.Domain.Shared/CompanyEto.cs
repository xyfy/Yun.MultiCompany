using System;
using System.Collections.Generic;
using System.Text;

namespace Yun.MultiCompany
{
    [Serializable]
    public class CompanyEto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}