using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yun.MultiCompany.EntityFrameworkCore
{
    public static class MultiCompanyEfCoreQueryableExtensions
    {
        public static IQueryable<Company> IncludeDetails(this IQueryable<Company> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(x => x.UserCompanys);
        }
    }
}
