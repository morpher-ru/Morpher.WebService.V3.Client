namespace Morpher.WebService.V3
{
    public class AccessDeniedException : System.Exception
    {
        protected AccessDeniedException(string errorMessage)
            : base(errorMessage)
        {
        }
    }
}