namespace Morpher.WebService.V3.Russian
{
    public class InvalidCaseValueException : InvalidArgumentException
    {
        public override string Message =>
            "Неправильное значение параметра case. Допустимые значения: nominative/genitive/dative/accusative/instrumental/prepositional.";

        public InvalidCaseValueException(string parameterName)
            : base(parameterName)
        {
        }
    }
}