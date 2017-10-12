namespace Morpher.WebService.V3.Exceptions
{
    public class IpBlockedException : MorpherException
    {
        private static readonly string ErrorMessage =
            "IP заблокирован.";

        public IpBlockedException()
            : base(ErrorMessage)
        {
        }
    }
}