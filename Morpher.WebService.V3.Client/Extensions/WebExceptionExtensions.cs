namespace Morpher.WebService.V3.Extensions
{
    using System.IO;
    using System.Net;

    internal static class WebExceptionExtensions
    {
        public static string GetResponseText(this WebException exception)
        {
            Stream responseStream = exception.Response.GetResponseStream();

            if (responseStream == null) return null;

            using (var reader = new StreamReader(responseStream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
