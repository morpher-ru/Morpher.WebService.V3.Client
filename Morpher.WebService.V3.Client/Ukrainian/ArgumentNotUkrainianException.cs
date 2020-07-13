namespace Morpher.WebService.V3.Ukrainian
{
    public class ArgumentNotUkrainianException : InvalidArgumentException
    {
        public override string Message =>
            "Не найдено украинских слов.";

        public ArgumentNotUkrainianException(string parameterName) 
            : base(parameterName)
        {
        }
    }
}