using System;
using System.Net;
using System.Text;
using Morpher.WebService.V3.Extensions;

namespace Morpher.WebService.V3
{
    class MyWebClient : IDisposable
    {
        readonly string _baseUrl;
        readonly WebClient _webClient;

        public MyWebClient (Guid? token, string baseUrl)
        {
            _baseUrl = baseUrl;
            _webClient = new WebClient { Encoding = Encoding.UTF8 };
            if (token != null)
            {
                AddParam("token", token.ToString());
            }
            AddParam("format", "json");
        }

        public void AddParam(string name, string value)
        {
            _webClient.QueryString.Add(name, value);
        }

        public T GetObject<T>(string relativeUrl)
        {
            return _webClient.GetObject<T>(_baseUrl + relativeUrl);
        }

        public void Dispose()
        {
            _webClient.Dispose();
        }
    }
}
