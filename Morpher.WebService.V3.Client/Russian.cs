namespace Morpher.WebService.V3
{
    using System;
    using System.Collections.Generic;

    public class Russian
    {
        readonly Func<MyWebClient> _newClient;

        internal Russian(Func<MyWebClient> newClient)
        {
            _newClient = newClient;
        }

        public RussianDeclensionResult Parse(string lemma, DeclensionFlags? flags = null)
        {
            using (var client = _newClient())
            {
                if (flags != null)
                {
                    client.AddParam("flags", flags.ToString().Replace(" ", string.Empty));
                }

                client.AddParam("s", lemma);

                var declensionResult = client.GetObject<RussianDeclensionResult>("/russian/declension");

                declensionResult.Nominative = lemma;

                return declensionResult;
            }
        }

        public RussianNumberSpellingResult Spell(uint number, string unit)
        {
            using (var client = _newClient())
            {
                client.AddParam("n", number.ToString());
                client.AddParam("unit", unit);

                return client.GetObject<RussianNumberSpellingResult>("/russian/spell");
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
    }
}