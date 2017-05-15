namespace Morpher.API.V3.Interfaces
{
    using System;

    public interface IMorpherClient
    {
        IRussian Russian { get; }

        IUkrainian Ukrainian { get; }

        int QueriesLeftForTodat(Guid? guid = null);
    }
}
