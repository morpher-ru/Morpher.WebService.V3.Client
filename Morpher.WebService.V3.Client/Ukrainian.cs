namespace Morpher.WebService.V3
{
    using System;
    using System.Net;
    using System.Text;

    using Morpher.WebService.V3.Extensions;

    public class Ukrainian
    {
        private readonly Guid? token = null;

        private readonly string url;

        public Ukrainian(string url, Guid? token = null)
        {
            this.token = token;
            this.url = url;
        }

        public UkrainianDeclensionResult Parse(string lemma, DeclensionFlags? flags = null)
        {
            using (WebClient client = new WebClient() { Encoding = Encoding.UTF8 })
            {
                if (this.token != null)
                {
                    client.QueryString.Add("token", this.token.ToString());
                }

                if (flags != null)
                {
                    client.QueryString.Add("flags", flags.ToString().Replace(" ", string.Empty));
                }

                client.QueryString.Add("format", "json");
                client.QueryString.Add("s", lemma);

                object result = client.GetObject(typeof(UkrainianDeclensionResult), $"{this.url}/ukrainian/declension");

                var declensionResult = result as UkrainianDeclensionResult;
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

        public UkrainianNumberSpellingResult Spell(uint number, string unit)
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


                object result = client.GetObject(typeof(UkrainianNumberSpellingResult), $"{this.url}/ukrainian/spell");

                var declensionResult = result as UkrainianNumberSpellingResult;
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
    }
}
