namespace Morpher.WebService.V3.Russian
{
    public class NumeralsDeclensionNotSupportedException : InvalidArgumentException
    {
        private static readonly string ErrorMessage =
            "Склонение числительных методом parse не поддерживается. Используйте метод spell.";

        public NumeralsDeclensionNotSupportedException()
            : base(ErrorMessage)
        {
        }
    }
}