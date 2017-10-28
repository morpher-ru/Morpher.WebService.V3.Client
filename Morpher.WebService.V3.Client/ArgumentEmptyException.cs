namespace Morpher.WebService.V3
{
    public class ArgumentEmptyException : InvalidArgumentException
    {
        public override string Message =>
            "Сервису передана пустая строка.";
    }
}