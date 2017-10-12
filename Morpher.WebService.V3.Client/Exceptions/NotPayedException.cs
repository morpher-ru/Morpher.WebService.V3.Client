namespace Morpher.WebService.V3.Exceptions
{
    public class NotPayedException : MorpherException
    {
        private static readonly string ErrorMessage =
            "Необходимо оплатить услугу.";

        public NotPayedException()
            : base(ErrorMessage)
        {
        }
    }
}