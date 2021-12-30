namespace Yun.MultiCompany.ConfigurationStore
{
    public class YunDefaultCompanyStoreOptions
    {
        public CompanyConfiguration[] Companies { get; set; }

        public YunDefaultCompanyStoreOptions()
        {
            Companies = new CompanyConfiguration[0];
        }
    }
}
