namespace Morpher.WebService.V3.Russian
{
    public class InvalidNbspValueException : InvalidArgumentException
    {
        public override string Message =>
            "Неправильное значение параметра nbsp. Допустимые значения: all/none.";

        public InvalidNbspValueException(string parameterName)
            : base(parameterName)
        {
        }
    }
}