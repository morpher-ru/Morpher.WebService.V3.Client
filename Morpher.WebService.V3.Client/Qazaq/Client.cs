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
    }
}
