using JetBrains.Annotations;
using System;

namespace Yun.MultiCompany
{
    public interface ICurrentCompany
    {
        bool IsAvailable { get; }

        [CanBeNull]
        Guid? Id { get; }

        [CanBeNull]
        string Name { get; }

        IDisposable Change(Guid? id, string name = null);
    }
}
