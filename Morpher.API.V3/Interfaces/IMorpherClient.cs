// ReSharper disable CheckNamespace
namespace Morpher.API.V3
// ReSharper restore CheckNamespace
{
    using System;

    public interface IMorpherClient
    {
        IRussian Russian { get; }

        IUkrainian Ukrainian { get; }

        int QueriesLeftForToday(Guid? guid = null);
    }
}