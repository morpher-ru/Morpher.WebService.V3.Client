namespace Morpher.WebService.V3
{
    public class TokenNotFoundException : AccessDeniedException
    {
        public override string Message =>
            "Данный token не найден.";
    }
}