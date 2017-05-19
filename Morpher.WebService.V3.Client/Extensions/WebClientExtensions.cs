namespace Morpher.WebService.V3.Extensions
{
    using System;
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization.Json;
    using System.Text;

    internal static class WebClientExtensions
    {
        public static T GetObject<T>(this WebClient client, string url)
        {
            try
            {
                string response = client.DownloadString(url);
                return Deserialize<T>(response);
            }
            catch (WebException exc)
            {
                string response = exc.GetResponseText();
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
    }
}
