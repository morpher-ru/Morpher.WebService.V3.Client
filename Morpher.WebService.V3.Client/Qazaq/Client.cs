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

        /// <summary>
        /// Строит количественное числительное прописью из данного числа.
        /// </summary>
        /// <param name="n">Число.</param>
        /// <param name="use-one">Чтобы вместо "бір мың" (одна тысяча) выдавалось упрощенная запись "мың" (тысяча), укажите параметр use-one=false.</param>
        /// <returns>Количественное числительное прописью, например, "жиырма бес"</returns>
        public string GetCardinal(long n, bool useOne)
        {
            using (var client = _newClient())
            {
                client.AddParam("n", n.ToString());
                client.AddParam("use-one", useOne.ToString());
                return client.GetObject<string>("/qazaq/cardinal");
            }
        }

        /// <summary>
        /// Строит порядковое числительное из данного количественного числительного.
        /// </summary>
        /// <param name="cardinal">Количественное числительное, например, "жиырма бес".</param>
        /// <returns>Порядковое числительное, например, "жиырма бесінші"</returns>
        /// <exception cref="ArgumentNotQazaqException">
        /// Если <paramref name="cardinal"/> не является казахским словом.
        /// </exception>
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

        /// <summary>
        /// Функция преобразует дату в формате ГГГГ-ММ-ДД в пропись на казахском языке.
        /// </summary>
        /// <param name="date">Дата, например, 2000-10-23.</param>
        /// <param name="use-one">Чтобы вместо "бір мың" (одна тысяча) выдавалось упрощенная запись "мың" (тысяча), укажите параметр use-one=false.</param>
        /// <returns>Дата прописью, например, "екі мыңыншы жылғы жиырма үшінші казан"</returns>
        /// <exception cref="QazaqWrongDateException">
        /// Если <paramref name="date"/> не соответствует формату.
        /// </exception>
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

        /// <summary>
        /// Функция преобразует дату в формате ГГГГ-ММ-ДД в пропись на казахском языке.
        /// </summary>
        /// <param name="date">Дата, например, 2000-10-23.</param>
        /// <param name="use-one">Чтобы вместо "бір мың" (одна тысяча) выдавалось упрощенная запись "мың" (тысяча), укажите параметр use-one=false.</param>
        /// <returns>Дата прописью, например, "екі мыңыншы жылғы жиырма үшінші казан"</returns>
        /// <exception cref="QazaqWrongDateException">
        /// Если <paramref name="date"/> не соответствует формату.
        /// </exception>
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

        /// <summary>
        /// Функция преобразует номер дня и месяца в пропись на казахском языке.
        /// </summary>
        /// <param name="day">Число от 1 до 31.</param>
        /// <param name="month">Номер месяца от 1 до 12.</param>
        /// <returns>День месяца прописью, например, "ақпанның оны"</returns>
        /// <exception cref="QazaqWrongDayOfMonthException">
        /// Если <paramref name="day"/> больше 31 или меньше 1.
        /// </exception>
        /// <exception cref="QazaqWrongMonthException">
        /// Если <paramref name="month"/> больше 12 или меньше 1.
        /// </exception>
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
