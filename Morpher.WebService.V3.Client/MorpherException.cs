using System;

namespace Morpher.WebService.V3
{
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