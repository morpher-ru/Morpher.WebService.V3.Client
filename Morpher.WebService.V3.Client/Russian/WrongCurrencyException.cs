namespace Morpher.WebService.V3.Russian
{
    public class WrongCurrencyException : InvalidArgumentException
    {
        public override string Message =>
            "Неправильное значение параметра currency.";

        public WrongCurrencyException(string parameterName)
            : base(parameterName)
        {
        }
    }
}