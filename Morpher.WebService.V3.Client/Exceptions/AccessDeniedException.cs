namespace Morpher.WebService.V3.Exceptions
{
    public class AccessDeniedException : MorpherException
    {
        protected AccessDeniedException(string errorMessage)
            : base(errorMessage)
        {
        }
    }
}