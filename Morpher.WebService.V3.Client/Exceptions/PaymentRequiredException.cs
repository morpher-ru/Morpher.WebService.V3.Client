namespace Morpher.WebService.V3.Exceptions
{
    public class PaymentRequiredException : MorpherException
    {
        private static readonly string ErrorMessage =
            "Необходимо оплатить услугу.";

        public PaymentRequiredException()
            : base(ErrorMessage)
        {
        }
    }
}