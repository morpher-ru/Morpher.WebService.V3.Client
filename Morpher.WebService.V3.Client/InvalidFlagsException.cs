namespace Morpher.WebService.V3
{
    public class InvalidFlagsException : InvalidArgumentException
    {
        public override string Message =>
            "Указаны неправильные флаги.";
    }
}