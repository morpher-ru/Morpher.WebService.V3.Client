namespace Morpher.WebService.V3.Russian
{
    public class ArgumentNotRussianException : InvalidArgumentException
    {
        private static readonly string ErrorMessage = "Не найдено русских слов.";

        public ArgumentNotRussianException()
            : base(ErrorMessage)
        {
        }
    }
}