using System;
using System.Collections.Generic;
using System.Text;

namespace Morpher.WebService.V3.Qazaq
{
    internal class QazaqWebClient : MyWebClient
    {
        public QazaqWebClient(Guid? token, string baseUrl, IWebClient webClient) : base(token, baseUrl, webClient)
        {

        }

        protected override void throwArgumentWrongLanguageException()
        {
            throw new ArgumentNotQazaqException();
        }
    }
}
