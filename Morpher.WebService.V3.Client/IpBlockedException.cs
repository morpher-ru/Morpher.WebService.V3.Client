namespace Morpher.WebService.V3
{
    public class IpBlockedException : AccessDeniedException
    {
        public override string Message =>
            "IP заблокирован.";
    }
}