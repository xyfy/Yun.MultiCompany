using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Users;

namespace Yun.MultiCompany
{
    public interface ICurrentCompanyAccessor
    {
        BasicCompanyInfo Current { get; set; }
    }
}
