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
            if (string.IsNullOrWhiteSpace(lemma))
            {
                throw new ArgumentEmptyException(nameof(lemma));
            }
            
            using (var client = _newClient())
            {
                client.AddParam("s", lemma);

                try
                {
                    var declensionResult = client.GetObject<DeclensionResult>("/qazaq/declension");
                    
                    declensionResult.Nominative = lemma;

                    return declensionResult;
                }
                catch (BadRequestException e) when (e.Status == 496)
                {
                    throw new ArgumentNotQazaqException(nameof(lemma));
                }
            }
        }

        public string GetCardinal(long n, bool useOne)
        {
            using (var client = _newClient())
            {
                client.AddParam("n", n.ToString());
                client.AddParam("use-one", useOne.ToString());

                var stringResult = client.GetObject<string>("/qazaq/cardinal");

                return stringResult;
            }
        }

        public string GetOrdinal(string cardinal)
        {
            using (var client = _newClient())
            {
                client.AddParam("cardinal", cardinal);

                var stringResult = client.GetObject<string>("/qazaq/ordinal");

                return stringResult;
            }
        }

        public string GetDate(DateTime dateTime, bool useOne)
        {
            using (var client = _newClient())
            {
                client.AddParam("date", dateTime.ToString());
                client.AddParam("use-one", useOne.ToString());
                var stringResult = client.GetObject<string>("/qazaq/date");

                return stringResult;
            }
        }

        public string GetDayOfMonth(int day, int month)
        {
            using (var client = _newClient())
            {
                client.AddParam("day", day.ToString());
                client.AddParam("month", month.ToString());

                var stringResult = client.GetObject<string>("/qazaq/day-of-month");

                return stringResult;
            }
        }
    }
}
