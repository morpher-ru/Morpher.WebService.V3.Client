namespace Morpher.WebService.V3.Qazaq
{
    public class QazaqWrongDateException : InvalidArgumentException
    {
        public override string Message =>
            "Неправильная дата. Ожидается формат ГГГГ-ММ-ДД.";

        public QazaqWrongDateException(string parameterName)
            : base(parameterName)
        {
        }
    }
}