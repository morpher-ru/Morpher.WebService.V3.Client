namespace Morpher.API.V3
{
    using System;
    using System.Net;
    using System.Text;

    using Morpher.API.V3.Extensions;
    using Morpher.API.V3;

    public class MorpherClient : IMorpherClient
    {
        private readonly string url;

        private readonly Guid? token;

        public MorpherClient(Guid? token = null, string url = null)
        {
            this.token = token;
            this.url = url ?? "http://api3.morpher.ru";
            this.Russian = new Russian(this.url, this.token);
            this.Ukrainian = new Ukrainian(this.url, this.token);
        }

        public IRussian Russian { get; }

        public IUkrainian Ukrainian { get; }

        public int QueriesLeftForToday(Guid? guid = null)
        {
            using (WebClient client = new WebClient() { Encoding = Encoding.UTF8 })
            {
                if (this.token != null)
                {
                    client.QueryString.Add("token", this.token.ToString());
                }

                client.QueryString.Add("format", "json");

                object result = client.GetObject(typeof(int), $"{this.url}/get_queries_left_for_today");

                try
                {
                    return (int)result;
                }
                catch (InvalidCastException)
                {
                    ServiceErrorMessage errorMessage = (ServiceErrorMessage)result;
                    throw new MorpherWebServiceException(errorMessage.Message, errorMessage.Code);
                }
            }
        }
    }
}
