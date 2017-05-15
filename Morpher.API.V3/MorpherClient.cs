namespace Morpher.API.V3
{
    using System;
    using System.Net;
    using System.Text;

    using Morpher.API.V3.Extensions;
    using Morpher.API.V3.Models;
    using Morpher.API.V3.Models.Exceptions;

    public class MorpherClient : IMorpherClient
    {
        private readonly Guid? token = null;

        private readonly string url = "http://api3.morpher.ru";

        public MorpherClient(Guid token, string url)
        {
            this.token = token;
            this.url = url;
        }

        public MorpherClient()
        {
        }

        public RussianDeclensionResult ParseRussian(string lemma, DeclensionFlags? flags = null)
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
                    return declensionResult;
                }
                else
                {
                    ServiceErrorMessage errorMessage = (ServiceErrorMessage)result;
                    throw new MorpherWebServiceException(errorMessage.Message, errorMessage.Code);
                }
            }
        }

        public UkrainianDeclensionResult ParseUkrainian(string lemma, DeclensionFlags? flags = null)
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

                object result = client.GetObject(typeof(UkrainianDeclensionResult), $"{this.url}/ukrainian/declension");

                var declensionResult = result as UkrainianDeclensionResult;
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

        public RussianNumberSpellingResult SpellRussian(uint number, string unit)
        {
            using (WebClient client = new WebClient() { Encoding = Encoding.UTF8 })
            {
                if (this.token != null)
                {
                    client.QueryString.Add("token", this.token.ToString());
                }

                client.QueryString.Add("number", number.ToString());
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

        public UkrainianNumberSpellingResult SpellUkrainian(uint number, string unit)
        {
            using (WebClient client = new WebClient() { Encoding = Encoding.UTF8 })
            {
                if (this.token != null)
                {
                    client.QueryString.Add("token", this.token.ToString());
                }

                client.QueryString.Add("number", number.ToString());
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
