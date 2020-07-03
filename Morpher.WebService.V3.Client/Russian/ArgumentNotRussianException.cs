namespace Morpher.WebService.V3.Russian
{
    public class ArgumentNotRussianException : InvalidArgumentException
    {
        public override string Message =>
            "Не найдено русских слов.";

        public ArgumentNotRussianException(string parameterName)
            : base(parameterName)
        {
        }
    }
}