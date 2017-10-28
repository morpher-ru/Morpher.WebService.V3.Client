namespace Morpher.WebService.V3
{
    public class DailyLimitExceededException : AccessDeniedException
    {
        public override string Message =>
            "Превышен лимит на количество запросов в сутки. Перейдите на следующий тарифный план.";
    }
}