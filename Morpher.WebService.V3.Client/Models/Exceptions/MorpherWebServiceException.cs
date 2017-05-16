namespace Morpher.WebService.V3
{
    using System;

    public class MorpherWebServiceException : Exception
    {
        public MorpherWebServiceException(string message, int code)
            : base(message)
        {
            this.Code = code;
        }

        public int Code { get; protected set; }
    }
}
