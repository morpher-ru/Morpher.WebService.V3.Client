namespace Morpher.WebService.V3
{
    using System;

    public class MorpherClient
    {
        private Func<MyWebClient> _newClient;

        private Russian.Client _russian;

        private Ukrainian.Client _ukrainian;

        public MorpherClient(Guid? token = null, string url = null)
        {
            this.Token = token;
            this.Url = url ?? "http://ws3.morpher.ru";
        }

        public Func<MyWebClient> NewClient
        {
            get => _newClient ?? (_newClient = () => new MyWebClient(Token, Url));
            set => _newClient = value ?? throw new ArgumentNullException(nameof(value));
        }

        public Russian.Client Russian => _russian ?? (_russian = new Russian.Client(NewClient));

        public Ukrainian.Client Ukrainian => _ukrainian ?? (_ukrainian = new Ukrainian.Client(NewClient));

        public string Url { get; }

        public Guid? Token { get; }

        public int QueriesLeftForToday(Guid? guid = null)
        {
            using (var client = NewClient())
            {
                return client.GetObject<int>("/get_queries_left_for_today");
            }
        }
    }
}
