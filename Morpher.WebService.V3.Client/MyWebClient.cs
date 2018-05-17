using System;
using System.IO;
using System.Net;
using System.Text;

namespace Morpher.WebService.V3
{
    using System.Collections.Specialized;
    using Newtonsoft.Json;

    internal class MyWebClient : IDisposable
    {
        readonly string _baseUrl;
        readonly IWebClient _webClient;

        public MyWebClient(Guid? token, string baseUrl, IWebClient webClient = null)
        {
            _baseUrl = baseUrl;
            _webClient = webClient ?? new MorpherWebClient { Encoding = Encoding.UTF8 };
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
                TryToThrowMorpherException(exc);
                throw;
            }
        }

        public void UploadValues(string relativeUrl, NameValueCollection collection)
        {
            try
            {
                _webClient.UploadValues(_baseUrl + relativeUrl, collection);
            }
            catch (WebException exc)
            {
                TryToThrowMorpherException(exc);
                throw;
            }
        }

        public void AddHeader(HttpRequestHeader header, string value)
        {
            _webClient.Headers.Add(header, value);
        }

        public T UploadString<T>(string relativeUrl, string data)
        {
            try
            {
                string response = _webClient.UploadString(_baseUrl + relativeUrl, data);
                
                return Deserialize<T>(response);
            }
            catch (WebException exc)
            {
                TryToThrowMorpherException(exc);
                throw;
            }
        }

        public T DeleteRequest<T>(string relativeUrl)
        {
            try
            {
                byte[] response = _webClient.UploadValues(_baseUrl + relativeUrl, "DELETE", new NameValueCollection());
                return Deserialize<T>(response);
            }
            catch (WebException exc)
            {
                TryToThrowMorpherException(exc);
                throw;
            }
        }

        static T Deserialize<T>(byte[] response)
        {
            using (var memoryStream = new MemoryStream(response))
            {
                try
                {
                    using (var reader = new StreamReader(memoryStream, Encoding.UTF8))
                    {
                        var serializer = new JsonSerializer();
                        return (T) serializer.Deserialize(reader, typeof(T));
                    }
                }
                catch (JsonReaderException)
                {
                    throw new InvalidServerResponseException();
                }
            }
        }

        static T Deserialize<T>(string response)
        {
            return Deserialize<T>(Encoding.UTF8.GetBytes(response));
        }

        static void TryToThrowMorpherException(WebException exc)
        {
            if (exc.Response is HttpWebResponse httpWebResponse 
                && (int)httpWebResponse.StatusCode >= 400 
                && (int)httpWebResponse.StatusCode < 500)
            {
                switch ((int)httpWebResponse.StatusCode)
                {
                    case 402: throw new DailyLimitExceededException();
                    case 403: throw new IpBlockedException();
                    case 495: throw new Russian.NumeralsDeclensionNotSupportedException();
                    case 496: throw new Russian.ArgumentNotRussianException();
                    case 400: throw new ArgumentEmptyException();
                    case 498: throw new TokenNotFoundException();
                    case 497: // "Неправильный формат токена". 
                        // Если мы такое получили, значит, ошибка в коде клиента или сервиса,
                        // но никак не ошибка пользователя.
                        throw new InvalidServerResponseException();
                    case 494: throw new InvalidFlagsException();
                    default: throw new InvalidServerResponseException();
                }
            }
        }
 
        public void Dispose()
        {
            _webClient.Dispose();
        }
    }
}
