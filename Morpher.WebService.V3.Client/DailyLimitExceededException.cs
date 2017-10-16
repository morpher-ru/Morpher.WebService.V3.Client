namespace Morpher.WebService.V3
{
    public class DailyLimitExceededException : AccessDeniedException
    {
        private static readonly string ErrorMessage =
            "Превышен лимит на количество запросов в сутки. Перейдите на следующий тарифный план.";

        public DailyLimitExceededException()
            : base(ErrorMessage)
        {
        }
    }
}