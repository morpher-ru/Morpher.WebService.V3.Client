namespace Morpher.WebService.V3.Qazaq
{
    public class ArgumentNotQazaqException : InvalidArgumentException
    {
        public override string Message =>
            "Не найдено казахских слов.";

        public ArgumentNotQazaqException(string parameterName)
            : base(parameterName)
        {
        }
    }
}