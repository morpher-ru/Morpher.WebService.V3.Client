namespace Morpher.WebService.V3.Exceptions
{
    public class ArgumentNotRussianException : MorpherException
    {
        private static readonly string ErrorMessage = "Не найдено русских слов.";

        public ArgumentNotRussianException()
            : base(ErrorMessage)
        {
        }
    }
}