namespace Morpher.WebService.V3.Qazaq
{
    public class OutOfRangeException : InvalidArgumentException
    {
        public override string Message =>
            "Недопустимое значение параметра n." +
            " Ожидается целое число в диапазоне +/−2 в 63 степени.";

        public OutOfRangeException(string parameterName)
            : base(parameterName)
        {
        }
    }
}