using System;

namespace Morpher.WebService.V3
{
    class UnknownResponseException : Exception
    {
        public int Status { get; }
        public string Body { get; }

        public UnknownResponseException(int status, string body)
        {
            Status = status;
            Body = body;
        }

        public override string Message => $"Сервис вернул ответ со статусом {Status} и телом: {Body}.";
    }
}