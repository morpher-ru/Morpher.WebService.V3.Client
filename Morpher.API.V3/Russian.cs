namespace Morpher.API.V3
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;

    using Morpher.API.V3.Extensions;
    using Morpher.API.V3;

    internal class Russian : IRussian
    {
        private readonly Guid? token = null;

        private readonly string url;

        public Russian(string url, Guid? token = null)
        {
            this.token = token;
            this.url = url;
        }

        public RussianDeclensionResult Parse(string lemma, DeclensionFlags? flags = null)
        {
            using (WebClient client = new WebClient() { Encoding = Encoding.UTF8 })
            {
                if (this.token != null)
                {
                    client.QueryString.Add("token", this.token.ToString());
                }

                if (flags != null)
                {
                    client.QueryString.Add("flags", ((int)flags).ToString());
                }

                client.QueryString.Add("format", "json");
                client.QueryString.Add("s", lemma);

                object result = client.GetObject(typeof(RussianDeclensionResult), $"{this.url}/russian/declension");

                var declensionResult = result as RussianDeclensionResult;
                if (declensionResult != null)
                {
                    declensionResult.Nominative = lemma;
                    return declensionResult;
                }
                else
                {
                    ServiceErrorMessage errorMessage = (ServiceErrorMessage)result;
                    throw new MorpherWebServiceException(errorMessage.Message, errorMessage.Code);
                }
            }
        }

        public RussianNumberSpellingResult Spell(uint number, string unit)
        {
            using (WebClient client = new WebClient() { Encoding = Encoding.UTF8 })
            {
                if (this.token != null)
                {
                    client.QueryString.Add("token", this.token.ToString());
                }

                client.QueryString.Add("n", number.ToString());
                client.QueryString.Add("unit", unit);
                client.QueryString.Add("format", "json");


                object result = client.GetObject(typeof(RussianNumberSpellingResult), $"{this.url}/russian/spell");

                var declensionResult = result as RussianNumberSpellingResult;
                if (declensionResult != null)
                {
                    return declensionResult;
                }
                else
                {
                    ServiceErrorMessage errorMessage = (ServiceErrorMessage)result;
                    throw new MorpherWebServiceException(errorMessage.Message, errorMessage.Code);
                }
            }
        }

        public AdjectiveGenders AdjectiveGenders(string lemma)
        {
            using (WebClient client = new WebClient() { Encoding = Encoding.UTF8 })
            {
                if (this.token != null)
                {
                    client.QueryString.Add("token", this.token.ToString());
                }

                client.QueryString.Add("format", "json");
                client.QueryString.Add("s", lemma);

                object result = client.GetObject(typeof(AdjectiveGenders), $"{this.url}/russian/genders");

                var adjectiveGenders = result as AdjectiveGenders;
                if (adjectiveGenders != null)
                {
                    return adjectiveGenders;
                }
                else
                {
                    ServiceErrorMessage errorMessage = (ServiceErrorMessage)result;
                    throw new MorpherWebServiceException(errorMessage.Message, errorMessage.Code);
                }
            }
        }

        public List<string> Adjectivize(string lemma)
        {
            using (WebClient client = new WebClient() { Encoding = Encoding.UTF8 })
            {
                if (this.token != null)
                {
                    client.QueryString.Add("token", this.token.ToString());
                }

                client.QueryString.Add("format", "json");
                client.QueryString.Add("s", lemma);

                object result = client.GetObject(typeof(List<string>), $"{this.url}/russian/adjectivize");

                var adjectives = result as List<string>;
                if (adjectives != null)
                {
                    return adjectives;
                }
                else
                {
                    ServiceErrorMessage errorMessage = (ServiceErrorMessage)result;
                    throw new MorpherWebServiceException(errorMessage.Message, errorMessage.Code);
                }
            }
        }
    }
}
