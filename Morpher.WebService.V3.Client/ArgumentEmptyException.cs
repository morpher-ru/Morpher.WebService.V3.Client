namespace Morpher.WebService.V3
{
    public class ArgumentEmptyException : InvalidArgumentException
    {
        private static readonly string ErrorMessage =
            "Сервису передана пустая строка.";

        public ArgumentEmptyException()
            : base(ErrorMessage)
        {
        }
    }
}