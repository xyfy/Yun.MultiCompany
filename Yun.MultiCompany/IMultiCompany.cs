using System;
using System.Collections.Generic;
using System.Text;

namespace Yun.MultiCompany
{
    public interface IMultiCompany
    {
        /// <summary>
        /// 用户现在切换到的公司id
        /// </summary>
        Guid? CompanyId { get; }
    }
}
