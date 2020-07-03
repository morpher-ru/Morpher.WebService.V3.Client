namespace Morpher.WebService.V3
{
    public class InvalidArgumentException : System.Exception
    {
        public string ParameterName { get; }

        public InvalidArgumentException(string parameterName)
        {
            ParameterName = parameterName;
        }
    }
}