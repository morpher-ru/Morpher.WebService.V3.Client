namespace Morpher.WebService.V3.Russian
{
    public class NumeralsDeclensionNotSupportedException : MorpherException
    {
        private static readonly string ErrorMessage =
            "Склонение числительных в declension не поддерживается. Используйте метод spell.";

        public NumeralsDeclensionNotSupportedException()
            : base(ErrorMessage)
        {
        }
    }
}