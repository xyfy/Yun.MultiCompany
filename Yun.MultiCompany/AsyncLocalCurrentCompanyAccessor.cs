using System.Threading;

namespace Yun.MultiCompany
{
    public partial class YunMultiCompanyModule
    {
        public class AsyncLocalCurrentCompanyAccessor : ICurrentCompanyAccessor
        {
            public static AsyncLocalCurrentCompanyAccessor Instance { get; } = new();

            public BasicCompanyInfo Current
            {
                get => _currentScope.Value;
                set => _currentScope.Value = value;
            }

            private readonly AsyncLocal<BasicCompanyInfo> _currentScope;

            private AsyncLocalCurrentCompanyAccessor()
            {
                _currentScope = new AsyncLocal<BasicCompanyInfo>();
            }
        }
    }
}
