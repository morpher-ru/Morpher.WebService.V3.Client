namespace Morpher.WebService.V3.Russian
{
    public class InvalidDelimValueException : InvalidArgumentException
    {
        public override string Message =>
            "Неправильное значение параметра delim. Допустимые значения: thinsp/space/none/comma/dot.";

        public InvalidDelimValueException(string parameterName)
            : base(parameterName)
        {
        }
    }
}