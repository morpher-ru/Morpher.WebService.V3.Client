namespace Morpher.WebService.V3.Russian
{
    /// <summary>
    /// Возникает, если в метод <see cref="Client.Parse(string, DeclensionFlags?)"/> передать числительное, например, "три поросенка".
    /// </summary>
    public class NumeralsDeclensionNotSupportedException : InvalidArgumentException
    {
        public override string Message => 
            "Склонение числительных методом parse не поддерживается. Используйте метод spell.";

        public NumeralsDeclensionNotSupportedException(string parameterName)
            : base(parameterName)
        {
        }
    }
}