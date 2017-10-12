namespace Morpher.WebService.V3.Exceptions
{
    public class InvalidTokenFormatException : MorpherException
    {
        private static readonly string ErrorMessage = "Неверный формат токена.";

        public InvalidTokenFormatException()
            : base(ErrorMessage)
        {
        }
    }
}