namespace Morpher.WebService.V3
{
    public class AccessDeniedException : MorpherException
    {
        protected AccessDeniedException(string errorMessage)
            : base(errorMessage)
        {
        }
    }
}