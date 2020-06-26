using System;

namespace Morpher.WebService.V3.Qazaq
{
    public class Client
    {
        readonly Func<MyWebClient> _newClient;

        internal Client(Func<MyWebClient> newClient)
        {
            _newClient = newClient;
        }

        public DeclensionResult Parse(string lemma)
        {
            using (var client = _newClient())
            {
                client.AddParam("s", lemma);

                var declensionResult = client.GetObject<DeclensionResult>("/qazaq/declension");
                
                declensionResult.Nominative = lemma;

                return declensionResult;
            }
        }

        public StringResult Cardinal(int n)
        {
            using (var client = _newClient())
            {
                client.AddParam("n", n.ToString());

                var stringResult = client.GetObject<StringResult>("/qazaq/cardinal");

                return stringResult;
            }
        }

        public StringResult Ordinal(string cardinal)
        {
            using (var client = _newClient())
            {
                client.AddParam("cardinal", cardinal);

                var stringResult = client.GetObject<StringResult>("/qazaq/ordinal");

                return stringResult;
            }
        }

        public StringResult Date(string date)
        {
            using (var client = _newClient())
            {
                client.AddParam("date", date);

                var stringResult = client.GetObject<StringResult>("/qazaq/date");

                return stringResult;
            }
        }

        public StringResult DayOfMonth(int day, int month)
        {
            using (var client = _newClient())
            {
                client.AddParam("day", day.ToString());
                client.AddParam("month", month.ToString());

                var stringResult = client.GetObject<StringResult>("/qazaq/day-of-month");

                return stringResult;
            }
        }
    }
}
