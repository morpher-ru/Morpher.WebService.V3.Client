using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Morpher.WebService.V3
{
    using System.Collections.Specialized;

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
            try
            {
                string response = _webClient.DownloadString(_baseUrl + relativeUrl);
                return Deserialize<T>(response);
            }
            catch (WebException exc)
            {
                string response = GetResponseText(exc);
                if (response == null) throw;
                var error = Deserialize<ServiceErrorMessage>(response);
                throw new MorpherWebServiceException(error.Message, error.Code);
            }
        }

        public void DeleteRequest(string relativeUrl)
        {
            try
            {
                _webClient.UploadValues(_baseUrl + relativeUrl, "DELETE", new NameValueCollection());
            }
            catch (WebException exc)
            {
                string response = GetResponseText(exc);
                if (response == null) throw;
                var error = Deserialize<ServiceErrorMessage>(response);
                throw new MorpherWebServiceException(error.Message, error.Code);
            }
        }

        static T Deserialize<T>(string response)
        {
            using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(response)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T) serializer.ReadObject(memoryStream);
            }
        }

        static string GetResponseText(WebException exception)
        {
            Stream responseStream = exception.Response?.GetResponseStream();

            if (responseStream == null) return null;

            using (var reader = new StreamReader(responseStream))
            {
                return reader.ReadToEnd();
            }
        }

        public void Dispose()
        {
            _webClient.Dispose();
        }
    }
}
