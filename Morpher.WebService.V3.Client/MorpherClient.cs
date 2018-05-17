namespace Morpher.WebService.V3
{
    using System;

    public class MorpherClient
    {
        private readonly IWebClient _webClient;

        public MorpherClient(Guid? token = null, string url = null, IWebClient webClient = null)
        {
            _webClient = webClient;
            this.Token = token;
            this.Url = url ?? "http://ws3.morpher.ru";
            Russian = new Russian.Client(NewClient);
            Ukrainian = new Ukrainian.Client(NewClient);
        }

        MyWebClient NewClient()
        {
            return new MyWebClient(Token, Url, _webClient);
        }

        public Russian.Client Russian { get; }

        public Ukrainian.Client Ukrainian { get; }

        string Url { get; }

        Guid? Token { get; }

        public int QueriesLeftForToday(Guid? guid = null)
        {
            using (var client = NewClient())
            {
                return client.GetObject<int>("/get_queries_left_for_today");
            }
        }
    }
}
