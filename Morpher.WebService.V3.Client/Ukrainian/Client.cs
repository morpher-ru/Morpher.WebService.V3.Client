using System;
using System.Net;
using Morpher.WebService.V3.Russian;

namespace Morpher.WebService.V3.Ukrainian
{
    using System.Globalization;

    public class Client
    {
        readonly Func<MyWebClient> _newClient;

        internal Client(Func<MyWebClient> newClient)
        {
            _newClient = newClient;
            UserDict = new UserDict(_newClient);
        }

        public UserDict UserDict { get; }

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
                    var declensionResult = client.GetObject<DeclensionResult>("/ukrainian/declension");

                    declensionResult.Nominative = lemma;

                    return declensionResult;
                }
                catch (BadRequestException e) when (e.Status == 494)
                {
                    // TODO throw new InvalidFlagsException(nameof(flags));
                    throw; 
                }
                catch (BadRequestException e) when (e.Status == 496)
                {
                    // TODO throw new ArgumentNotUkrainianException(nameof(lemma));
                    throw;
                }
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
                client.AddParam("n", number.ToString(CultureInfo.InvariantCulture));
                client.AddParam("unit", unit);

                return client.GetObject<NumberSpellingResult>("/ukrainian/spell");
            }
        }
    }
}
