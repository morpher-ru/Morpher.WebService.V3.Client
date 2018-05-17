namespace Morpher.WebService.V3
{
    using System;

    public class MorpherClient
    {
        private readonly string _url;
        private readonly Guid? _token;
        private readonly IWebClient _webClient;

        public MorpherClient(Guid? token = null, string url = null, IWebClient webClient = null)
        {
            _webClient = webClient;
            _token = token;
            _url = url ?? "http://ws3.morpher.ru";
            Russian = new Russian.Client(NewClient);
            Ukrainian = new Ukrainian.Client(NewClient);
        }

        MyWebClient NewClient() => new MyWebClient(_token, _url, _webClient);

        public Russian.Client Russian { get; }

        public Ukrainian.Client Ukrainian { get; }

        public int QueriesLeftForToday(Guid? guid = null)
        {
            using (var client = NewClient())
            {
                return client.GetObject<int>("/get_queries_left_for_today");
            }
        }
    }
}
