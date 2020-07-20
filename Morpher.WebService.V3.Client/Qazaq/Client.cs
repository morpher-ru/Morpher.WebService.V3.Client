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
                try
                {
                    return client.GetObject<string>("/qazaq/cardinal");
                }
                catch (BadRequestException e) when (e.ErrorCode == 19)
                {
                    throw new OutOfRangeException(nameof(n));
                }
            }
        }

        public string GetOrdinal(string cardinal)
        {
            using (var client = _newClient())
            {
                client.AddParam("cardinal", cardinal);

                try
                {
                    return client.GetObject<string>("/qazaq/ordinal");
                }
                catch (BadRequestException e) when (e.ErrorCode == 15)
                {
                    throw new ArgumentNotQazaqException(nameof(cardinal));
                }
            }
        }

        public string GetDate(string date, bool useOne)
        {
            using (var client = _newClient())
            {
                client.AddParam("date", date);
                client.AddParam("use-one", useOne.ToString());
                var stringResult = client.GetObject<string>("/qazaq/date");
                try
                {
                    return client.GetObject<string>("/qazaq/date");
                }
                catch (BadRequestException e) when (e.ErrorCode == 16)
                {
                    throw new QazaqWrongDateException(nameof(date));
                }
            }
        }

        public string GetDate(DateTime dateTime, bool useOne)
        {
            using (var client = _newClient())
            {
                client.AddParam("date", dateTime.ToString("yyyy-MM-dd"));
                client.AddParam("use-one", useOne.ToString());
                try
                {
                    return client.GetObject<string>("/qazaq/date");
                }
                catch (BadRequestException e) when (e.ErrorCode == 16)
                {
                    throw new QazaqWrongDateException(nameof(dateTime));
                }
            }
        }

        public string GetDayOfMonth(int day, int month)
        {
            using (var client = _newClient())
            {
                client.AddParam("day", day.ToString());
                client.AddParam("month", month.ToString());
                try
                {
                    return client.GetObject<string>("/qazaq/day-of-month");
                }
                catch (BadRequestException e)
                {
                    switch (e.ErrorCode)
                    {
                        case 17: throw new QazaqWrongDayOfMonthException(nameof(day));
                        case 18: throw new QazaqWrongMonthException(nameof(month));
                    }
                    throw;
                }
            }
        }
    }
}
