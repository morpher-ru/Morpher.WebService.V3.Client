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

        public void AddOrUpdateCorrection(CorrectionEntry entry)
        {
            if (string.IsNullOrWhiteSpace(entry.Singular.Nominative))
            {
                throw new ArgumentException("Нужно указать именительную форму единственного числа.", nameof(entry.Singular.Nominative));
            }

            var collection = entry.ToNameValueCollection();
            if (collection.Count <= 1)
            {
                throw new ArgumentException("Нужно указать как минимум одну форму кроме именительного падежа.", nameof(entry));

            }

            using (var client = _newClient())
            { 
                client.UploadValues("/russian/userdict", collection); 
            }
        }

        public void RemoveCorrection(string nominativeForm)
        {
            using (var client = _newClient())
            {
                client.AddParam("s", nominativeForm);
                client.DeleteRequest("/russian/userdict");
            }
        }

        public IEnumerable<CorrectionEntry> GetAllCorrections()
        {
            using (var client = _newClient())
            {
                return client.GetObject<IEnumerable<CorrectionEntry>>("/russian/userdict");
            }
        }
    }
}