namespace Morpher.WebService.V3.Extensions
{
    using System;
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization.Json;
    using System.Text;

    internal static class WebClientExtensions
    {
        public static object GetObject(this WebClient client, Type type, string url)
        {
            string response;

            try
            {
                response = client.DownloadString(url);
            }
            catch (WebException exc)
            {
                type = typeof(ServiceErrorMessage);
                response = exc.GetResponseText();
                if (response == null) throw;
            }

            using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(response)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(type);
                return serializer.ReadObject(memoryStream);
            }
        }
    }
}
