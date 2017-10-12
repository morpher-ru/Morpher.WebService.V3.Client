namespace Morpher.WebService.V3.Exceptions
{
    public class RussianWordsNotFoundException : MorpherException
    {
        private static readonly string ErrorMessage = "Не найдено русских слов.";

        public RussianWordsNotFoundException()
            : base(ErrorMessage)
        {
        }
    }
}