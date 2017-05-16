namespace Morpher.API.V3
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [Flags]
    [SuppressMessage("ReSharper", "StyleCop.SA1602")]
    public enum DeclensionFlags : byte
    {
        FullName = 1 << 0,
        Common = 1 << 1,
        Feminine = 1 << 2,
        Masculine = 1 << 3,
        Neuter = 1 << 4,
        Animate = 1 << 5,
        Inanimate = 1 << 6,
        Plural = 1 << 7
    }
}
