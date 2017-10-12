namespace Morpher.WebService.V3.Exceptions
{
    public class TokenNotFoundException : MorpherException
    {
        private static readonly string ErrorMessage = "Данный token не найден.";

        public TokenNotFoundException()
            : base(ErrorMessage)
        {
        }
    }
}