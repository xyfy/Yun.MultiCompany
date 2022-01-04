using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yun.MultiCompany
{
    public class MultiCompanyDomainMappingProfile : Profile
    {
        public MultiCompanyDomainMappingProfile()
        {
            CreateMap<Company, CompanyConfiguration>()
                .ForMember(x => x.IsActive, x => x.Ignore())
                ;

            CreateMap<Company, CompanyEto>();
        }
    }
}
