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

        public MyWebClient(Guid? token, string baseUrl, IWebClient webClient)
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
            _webClient.QueryString.Add(name, Uri.EscapeDataString(value));
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
                catch (JsonReaderException e)
                {
                    throw new InvalidServerResponseException(e);
                }
            }
        }

        static T Deserialize<T>(string response)
        {
            return Deserialize<T>(Encoding.UTF8.GetBytes(response));
        }

        static void TryToThrowMorpherException(WebException exc)
        {
            if (exc.Response is HttpWebResponse httpWebResponse)
            {
                int status = (int)httpWebResponse.StatusCode;
                int errorCode = int.Parse(httpWebResponse.Headers.Get("Error-Code"));
                
                if (status >= 400 && status < 500)
                {
                    switch (status)
                    {
                        // Здесь обрабатываются только ошибки, общие для всех методов.
                        // Специфичные ошибки обрабатываются в самих методах.
                        case 402: throw new DailyLimitExceededException();
                        case 403: throw new IpBlockedException();
                        case 498: throw new TokenNotFoundException();
                        case 497: // "Неправильный формат токена". 
                            // Если мы такое получили, значит, ошибка в коде клиента или сервиса,
                            // но никак не ошибка пользователя.
                            throw new InvalidServerResponseException(exc);
                    }

                    throw new BadRequestException(status, errorCode);
                }

                if (status >= 500)
                {
                    Stream responseStream = httpWebResponse.GetResponseStream();
                
                    string body = GetBody(responseStream);

                    string message = $"Got a response with status code {status} {httpWebResponse.StatusDescription}."
                                     + $"Status: {exc.Status}. Body:\n{body}";
                                         
                    throw new Exception(message, exc);
                }
            }
        }

        private static string GetBody(Stream responseStream)
        {
            if (responseStream == null)
                return "<empty>";
            
            try
            {
                return new StreamReader(responseStream).ReadToEnd();
            }
            catch (Exception e)
            {
                return "Failed to read the response stream: " + e.Message;
            }
        }

        public void Dispose()
        {
            _webClient.Dispose();
        }
    }
}
