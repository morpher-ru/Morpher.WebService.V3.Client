namespace Morpher.WebService.V3.Exceptions
{
    public class ExceededDailyLimitException : MorpherException
    {
        private static readonly string ErrorMessage =
            "Превышен лимит на количество запросов в сутки. Перейдите на следующий тарифный план.";

        public ExceededDailyLimitException()
            : base(ErrorMessage)
        {
        }
    }
}