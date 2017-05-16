namespace Morpher.WebService.V3.Extensions
{
    using System.IO;
    using System.Net;

    internal static class WebExceptionExtensions
    {
        public static string GetResponseText(this WebException exception)
        {
            using (var reader = new StreamReader(exception.Response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
