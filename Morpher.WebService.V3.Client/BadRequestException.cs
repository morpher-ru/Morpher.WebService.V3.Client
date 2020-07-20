using System;

namespace Morpher.WebService.V3
{
    class BadRequestException : Exception
    {
        public int Status { get; }
        public int ErrorCode { get; }

        public BadRequestException(int status, int errorCode)
        {
            Status = status;
            ErrorCode = errorCode;
        }

        public BadRequestException(int status)
        {
            Status = status;
        }
    }
}