namespace Morpher.WebService.V3.Exceptions
{
    using System;

    public class MorpherException : Exception
    {
        public MorpherException()
        {
        }

        public MorpherException(string message)
            : base(message)
        {
        }
    }
}