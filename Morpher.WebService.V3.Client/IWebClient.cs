namespace Morpher.WebService.V3
{
    using System.Collections.Specialized;

    interface IWebClient
    {
        NameValueCollection QueryString { get; set; }

        string DownloadString(string address);
        byte[] UploadValues(string address, NameValueCollection data);
        byte[] UploadValues(string address, string method, NameValueCollection data);
        void Dispose();
    }
}
