namespace Morpher.WebService.V3
{
    using System;
    using System.Net;
    using System.Text;

    using Morpher.WebService.V3.Extensions;

    public class MorpherClient
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

        public Russian Russian { get; }

        public Ukrainian Ukrainian { get; }

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

                if (result is int)
                {
                    return (int)result;
                }
                else
                {
                    ServiceErrorMessage errorMessage = (ServiceErrorMessage)result;
                    throw new MorpherWebServiceException(errorMessage.Message, errorMessage.Code);
                }
            }
        }
    }
}
