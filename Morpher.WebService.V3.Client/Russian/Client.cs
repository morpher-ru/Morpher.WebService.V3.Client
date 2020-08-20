using System;
using System.Collections.Generic;

namespace Morpher.WebService.V3.Russian
{
    using System.Globalization;
    using System.Net;

    public class Client
    {
        readonly Func<MyWebClient> _newClient;

        internal Client(Func<MyWebClient> newClient)
        {
            _newClient = newClient;
            UserDict = new UserDict(_newClient);
        }

        public UserDict UserDict { get; }

        public DeclensionResult Parse(string lemma, DeclensionFlags? flags = null)
        {
            if (string.IsNullOrWhiteSpace(lemma))
            {
                throw new ArgumentEmptyException(nameof(lemma));
            }
            
            using (var client = _newClient())
            {
                if (flags != null)
                {
                    client.AddParam("flags", flags.ToString().Replace(" ", string.Empty));
                }

                client.AddParam("s", lemma);

                try
                {
                    var declensionResult = client.GetObject<DeclensionResult>("/russian/declension");

                    declensionResult.Nominative = lemma;

                    return declensionResult;
                }
                catch (BadRequestException e)
                {
                    switch (e.Status)
                    {
                        case 495: throw new NumeralsDeclensionNotSupportedException(nameof(lemma));
                        case 496: throw new ArgumentNotRussianException(nameof(lemma));
                        case 494: throw new InvalidFlagsException(nameof(flags));
                    }

                    throw;
                }
            }
        }

        public IEnumerable<ResultOrError> Parse(
            IEnumerable<string> words,
            DeclensionFlags? flags = null)
        {
            using (var client = _newClient())
            {
                if (flags != null)
                {
                    client.AddParam("flags", flags.ToString().Replace(" ", string.Empty));
                }

                client.AddHeader(HttpRequestHeader.ContentType, "text/plain");
                return client.UploadString<IEnumerable<ResultOrError>>("/russian/declension",
                    string.Join("\n", words));
            }
        }

        public string AddStressMarks(string text)
        {
            using (var client = _newClient())
            {
                client.AddHeader(HttpRequestHeader.ContentType, "text/plain");
                return client.UploadString<string>("/russian/addstressmarks", text);
            }
        }

        public NumberSpellingResult Spell(decimal number, string unit)
        {
            if (string.IsNullOrWhiteSpace(unit))
            {
                throw new ArgumentEmptyException(nameof(unit));
            }
            
            using (var client = _newClient())
            {
                client.AddParam("n", number.ToString(new CultureInfo("en-US")));
                client.AddParam("unit", unit);

                try
                {
                    return client.GetObject<NumberSpellingResult>("/russian/spell");
                }
                catch (BadRequestException e) when (e.Status == 496)
                {
                    throw new ArgumentNotRussianException(nameof(unit));
                }
            }
        }

        public NumberSpellingResult SpellOrdinal(long number, string unit)
        {
            if (string.IsNullOrWhiteSpace(unit))
            {
                throw new ArgumentEmptyException(nameof(unit));
            }
            
            using (var client = _newClient())
            {
                client.AddParam("n", number.ToString());
                client.AddParam("unit", unit);

                try
                {
                    return client.GetObject<NumberSpellingResult>("/russian/spell-ordinal");
                }
                catch (BadRequestException e) when (e.Status == 496)
                {
                    throw new ArgumentNotRussianException(nameof(unit));
                }
            }
        }

        public DateSpellingResult SpellDate(DateTime date)
        {
            using (var client = _newClient())
            {
                string dateString = date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                client.AddParam("date", dateString);
                    return client.GetObject<DateSpellingResult>("/russian/spell-date");
            }
        }

        public AdjectiveGenders AdjectiveGenders(string lemma)
        {
            if (string.IsNullOrWhiteSpace(lemma))
            {
                throw new ArgumentEmptyException(nameof(lemma));
            }
            
            using (var client = _newClient())
            {
                client.AddParam("s", lemma);

                return client.GetObject<AdjectiveGenders>("/russian/genders");
            }
        }

        public List<string> Adjectivize(string lemma)
        {
            if (string.IsNullOrWhiteSpace(lemma))
            {
                throw new ArgumentEmptyException(nameof(lemma));
            }
            
            using (var client = _newClient())
            {
                client.AddParam("s", lemma);

                return client.GetObject<List<string>>("/russian/adjectivize");
            }
        }

        public Propis GetPropis(decimal n, string currency,
            string padeg, string capitals, string nbsp, string delim)
        {
            if (string.IsNullOrWhiteSpace(currency))
            {
                throw new ArgumentEmptyException(nameof(currency));
            }

            using (var client = _newClient())
            {

                try
                {
                    client.AddParam("n", n.ToString(CultureInfo.InvariantCulture));
                    client.AddParam("currency", currency);
                    client.AddParam("case", padeg);
                    client.AddParam("capitals", capitals);
                    client.AddParam("nbsp", nbsp);
                    client.AddParam("delim", delim);

                    return client.GetObject<Propis>("/russian/propis");
                }
                catch (BadRequestException e)
                {
                    switch (e.ErrorCode)
                    {
                        case 20: throw new WrongCurrencyException(nameof(currency));
                        case 21: throw new InvalidCapitalsValueException(nameof(capitals));
                        case 22: throw new InvalidCaseValueException("case");
                        case 23: throw new InvalidDelimValueException(nameof(delim));
                        case 24: throw new InvalidNbspValueException(nameof(nbsp));
                    }

                    throw;
                }
            }
        }
    }
}