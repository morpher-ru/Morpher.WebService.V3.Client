using System;
using System.Collections.Generic;

namespace Morpher.WebService.V3.Russian
{
    public class Client
    {
        readonly Func<MyWebClient> _newClient;

        internal Client(Func<MyWebClient> newClient)
        {
            _newClient = newClient;
        }

        public DeclensionResult Parse(string lemma, DeclensionFlags? flags = null)
        {
            using (var client = _newClient())
            {
                if (flags != null)
                {
                    client.AddParam("flags", flags.ToString().Replace(" ", string.Empty));
                }

                client.AddParam("s", lemma);

                var declensionResult = client.GetObject<DeclensionResult>("/russian/declension");

                declensionResult.Nominative = lemma;

                return declensionResult;
            }
        }

        public NumberSpellingResult Spell(uint number, string unit)
        {
            using (var client = _newClient())
            {
                client.AddParam("n", number.ToString());
                client.AddParam("unit", unit);

                return client.GetObject<NumberSpellingResult>("/russian/spell");
            }
        }

        public AdjectiveGenders AdjectiveGenders(string lemma)
        {
            using (var client = _newClient())
            {
                client.AddParam("s", lemma);

                return client.GetObject<AdjectiveGenders>("/russian/genders");
            }
        }

        public List<string> Adjectivize(string lemma)
        {
            using (var client = _newClient())
            {
                client.AddParam("s", lemma);

                return client.GetObject<List<string>>("/russian/adjectivize");
            }
        }

        public void AddOrUpdateCorrection()
        {
            throw new NotImplementedException();
        }

        public void RemoveCorrection()
        {
            throw new NotImplementedException();
        }

        public CorrectionEntry GetAllCorrections()
        {
            using (var client = _newClient())
            {
                return client.GetObject<CorrectionEntry>("/russian/userdict");
            }
        }
    }
}