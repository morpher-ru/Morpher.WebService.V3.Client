using System;
using System.IO;
using System.Net;
using System.Text;

namespace Morpher.WebService.V3
{
    using System.Collections.Specialized;
    using Exceptions;
    using Newtonsoft.Json;

    public class MyWebClient : IDisposable
    {
        readonly string _baseUrl;
        IWebClient _webClient;

        public MyWebClient(Guid? token, string baseUrl)
        {
            _baseUrl = baseUrl;
            if (token != null)
            {
                AddParam("token", token.ToString());
            }

            AddParam("format", "json");

        }

        public IWebClient WebClient
        {
            get { return _webClient ?? (_webClient = new MorpherWebClient() {Encoding = Encoding.UTF8}); }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _webClient = value;
            }
        }

        public void AddParam(string name, string value)
        {
            WebClient.QueryString.Add(name, value);
        }

        public T GetObject<T>(string relativeUrl)
        {
            try
            {
                string response = WebClient.DownloadString(_baseUrl + relativeUrl);
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
                WebClient.UploadValues(_baseUrl + relativeUrl, collection);
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
                var response = WebClient.UploadValues(_baseUrl + relativeUrl, "DELETE", new NameValueCollection());
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
            using (MemoryStream memoryStream = new MemoryStream(response))
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

        static T Deserialize<T>(string response)
        {
            return Deserialize<T>(Encoding.UTF8.GetBytes(response));
        }

        private void TryToThrowMorpherException(WebException exc)
        {
            var httpWebResponse = exc.Response as HttpWebResponse;
           
            if (httpWebResponse != null 
                && (int)httpWebResponse.StatusCode >= 400 
                && (int)httpWebResponse.StatusCode < 500)
            {
                switch ((int)httpWebResponse.StatusCode)
                {
                    case 402: throw new DailyLimitExceededException();
                    case 403: throw new IpBlockedException();
                    case 495: throw new NumeralsDeclensionNotSupportedException();
                    case 496: throw new ArgumentNotRussianException();
                    case 400: throw new RequiredParameterIsNotSpecifiedException();
                    case 498: throw new TokenNotFoundException();
                    case 497: throw new InvalidTokenFormatException();
                    case 494: throw new InvalidFlagsException();
                    default: throw new InvalidServerResponseException();
                }
            }
        }
 
        public void Dispose()
        {
            WebClient.Dispose();
        }
    }
}
