namespace Morpher.WebService.V3
{
    using System.Collections.Specialized;
    using System.Net;

    public interface IWebClient
    {
        NameValueCollection QueryString { get; set; }

        string DownloadString(string address);
        byte[] UploadValues(string address, NameValueCollection data);
        byte[] UploadValues(string address, string method, NameValueCollection data);
        string UploadString(string address, string data);
        void Dispose();
        WebHeaderCollection Headers { get; set; }
    }
}
