using System;

namespace Morpher.WebService.V3
{
    public class InvalidServerResponseException : System.Exception
    {
        private static readonly string ErrorMessage = "Сервер вернул неожиданный код. Возможно, у вас неактуальная версия клиента.";

        public InvalidServerResponseException(Exception inner)
            : base(ErrorMessage, inner)
        {
        }
    }
}
