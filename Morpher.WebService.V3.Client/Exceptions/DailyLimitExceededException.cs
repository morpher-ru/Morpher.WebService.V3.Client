namespace Morpher.WebService.V3.Exceptions
{
    public class DailyLimitExceededException : MorpherException
    {
        private static readonly string ErrorMessage =
            "Превышен лимит на количество запросов в сутки. Перейдите на следующий тарифный план.";

        public DailyLimitExceededException()
            : base(ErrorMessage)
        {
        }
    }
}