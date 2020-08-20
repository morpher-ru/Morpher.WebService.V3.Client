namespace Morpher.WebService.V3.Russian
{
    public class InvalidCapitalsValueException : InvalidArgumentException
    {
        public override string Message =>
            "Неправильное значение параметра capitals. Допустимые значения: frist/all/none.";

        public InvalidCapitalsValueException(string parameterName)
            : base(parameterName)
        {
        }
    }
}