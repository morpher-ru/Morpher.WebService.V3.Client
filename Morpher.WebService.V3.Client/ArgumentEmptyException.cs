namespace Morpher.WebService.V3
{
    public class ArgumentEmptyException : InvalidArgumentException
    {
        public override string Message =>
            $"В качестве параметра '{ParameterName}' передана пустая строка.";

        public ArgumentEmptyException(string parameterName)
            : base (parameterName)
        {
        }
    }
}