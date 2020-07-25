namespace Morpher.WebService.V3.Qazaq
{
    public class QazaqWrongDayOfMonthException : InvalidArgumentException
    {
        public override string Message =>
            "Недопустимое значение числового параметра day. Допустимы значения от 1 до 31 включительно.";

        public QazaqWrongDayOfMonthException(string parameterName)
            : base(parameterName)
        {
        }
    }
}