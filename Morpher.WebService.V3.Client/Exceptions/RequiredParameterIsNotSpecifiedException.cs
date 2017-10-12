namespace Morpher.WebService.V3.Exceptions
{
    public class RequiredParameterIsNotSpecifiedException : MorpherException
    {
        private static readonly string ErrorMessage =
            "Не указан обязательный параметр";

        public RequiredParameterIsNotSpecifiedException()
            : base(ErrorMessage)
        {
        }
    }
}