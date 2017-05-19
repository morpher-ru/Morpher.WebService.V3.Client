namespace Morpher.WebService.V3
{
    using System;

    public class Ukrainian
    {
        readonly Func<MyWebClient> _newClient;

        internal Ukrainian(Func<MyWebClient> newClient)
        {
            _newClient = newClient;
        }

        public UkrainianDeclensionResult Parse(string lemma, DeclensionFlags? flags = null)
        {
            using (var client = _newClient())
            {
                if (flags != null)
                {
                    client.AddParam("flags", flags.ToString().Replace(" ", string.Empty));
                }

                client.AddParam("s", lemma);

                var declensionResult = client.GetObject<UkrainianDeclensionResult>("/ukrainian/declension");

                declensionResult.Nominative = lemma;

                return declensionResult;
            }
        }

        public UkrainianNumberSpellingResult Spell(uint number, string unit)
        {
            using (var client = _newClient())
            {
                client.AddParam("n", number.ToString());
                client.AddParam("unit", unit);

                return client.GetObject<UkrainianNumberSpellingResult>("/ukrainian/spell");
            }
        }
    }
}
