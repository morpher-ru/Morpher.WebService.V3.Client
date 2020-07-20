namespace Morpher.WebService.V3.Qazaq
{
    public class QazaqWrongMonthException : InvalidArgumentException
    {
        public override string Message =>
            "Недопустимое значение числового параметра month. Допустимы значения от 1 до 12 включительно.";

        public QazaqWrongMonthException(string parameterName)
            : base(parameterName)
        {
        }
    }
}