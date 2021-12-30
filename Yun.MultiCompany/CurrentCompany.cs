using System;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Yun.MultiCompany
{
    public class CurrentCompany : ICurrentCompany, ITransientDependency
    {
        public virtual bool IsAvailable => Id.HasValue;

        public virtual Guid? Id => _currentCompanyAccessor.Current?.CompanyId;

        public string Name => _currentCompanyAccessor.Current?.Name;

        private readonly ICurrentCompanyAccessor _currentCompanyAccessor;

        public CurrentCompany(ICurrentCompanyAccessor currentCompanyAccessor)
        {
            _currentCompanyAccessor = currentCompanyAccessor;
        }

        public IDisposable Change(Guid? id, string name = null)
        {
            return SetCurrent(id, name);
        }

        private IDisposable SetCurrent(Guid? companyId, string name = null)
        {
            var parentScope = _currentCompanyAccessor.Current;
            _currentCompanyAccessor.Current = new BasicCompanyInfo(companyId, name);
            return new DisposeAction(() =>
            {
                _currentCompanyAccessor.Current = parentScope;
            });
        }
    }
}
