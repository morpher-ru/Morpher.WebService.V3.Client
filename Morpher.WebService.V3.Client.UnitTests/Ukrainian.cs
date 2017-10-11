namespace Morpher.WebService.V3.Client.UnitTests
{
    using System.Collections.Specialized;
    using System.IO;
    using System.Net;
    using System.Text;
    using Exceptions;
    using Moq;
    using NUnit.Framework;
    using V3.Ukrainian;

    [TestFixture]
    public class Ukrainian
    {
        public string DeclensionResultText { get; } = @"
{
    ""Р"": ""теста"",
    ""Д"": ""тесту"",
    ""З"": ""теста"",
    ""О"": ""тестом"",
    ""М"": ""тесті"",
    ""К"": ""тесте"",
    ""рід"": ""Чоловічий""
}";

        public string SpellText { get; } = @"
{
    ""n"": {
        ""Н"": ""десять"",
        ""Р"": ""десяти"",
        ""Д"": ""десяти"",
        ""З"": ""десять"",
        ""О"": ""десятьма"",
        ""М"": ""десяти"",
        ""К"": ""десять""
    },
    ""unit"": {
        ""Н"": ""рублів"",
        ""Р"": ""рублів"",
        ""Д"": ""рублям"",
        ""З"": ""рублів"",
        ""О"": ""рублями"",
        ""М"": ""рублях"",
        ""К"": ""рублів""
    }
}";



        [Test]
        public void Parse_Success()
        {
            Mock<IWebClient> webClient = new Mock<IWebClient>();
            webClient.Setup(client => client.QueryString).Returns(new NameValueCollection());
            webClient.Setup(client => client.DownloadString(It.IsAny<string>())).Returns(DeclensionResultText);
            MorpherClient morpherClient = new MorpherClient();
            morpherClient.NewClient = () => new MyWebClient(morpherClient.Token, morpherClient.Url)
            {
                WebClient = webClient.Object
            };

            DeclensionResult declensionResult = morpherClient.Ukrainian.Parse("тест");
            Assert.IsNotNull(declensionResult);
            Assert.AreEqual("тест", declensionResult.Nominative);
            Assert.AreEqual("теста", declensionResult.Genitive);
            Assert.AreEqual("тесту", declensionResult.Dative);
            Assert.AreEqual("теста", declensionResult.Accusative);
            Assert.AreEqual("тестом", declensionResult.Instrumental);
            Assert.AreEqual("тесті", declensionResult.Prepositional);
            Assert.AreEqual("тесте", declensionResult.Vocative);
            Assert.AreEqual(Gender.Masculine, declensionResult.Gender);
        }

        [Test]
        public void Spell_Success()
        {
            Mock<IWebClient> webClient = new Mock<IWebClient>();
            webClient.Setup(client => client.QueryString).Returns(new NameValueCollection());
            webClient.Setup(client => client.DownloadString(It.IsAny<string>())).Returns(SpellText);
            MorpherClient morpherClient = new MorpherClient();
            morpherClient.NewClient = () => new MyWebClient(morpherClient.Token, morpherClient.Url)
            {
                WebClient = webClient.Object
            };

            NumberSpellingResult declensionResult = morpherClient.Ukrainian.Spell(10, "рубль");
            Assert.IsNotNull(declensionResult);
            
            // number
            Assert.AreEqual("десять", declensionResult.NumberDeclension.Nominative);
            Assert.AreEqual("десяти", declensionResult.NumberDeclension.Genitive);
            Assert.AreEqual("десяти", declensionResult.NumberDeclension.Dative);
            Assert.AreEqual("десять", declensionResult.NumberDeclension.Accusative);
            Assert.AreEqual("десятьма", declensionResult.NumberDeclension.Instrumental);
            Assert.AreEqual("десяти", declensionResult.NumberDeclension.Prepositional);
            Assert.AreEqual("десять", declensionResult.NumberDeclension.Vocative);

            // unit
            Assert.AreEqual("рублів", declensionResult.UnitDeclension.Nominative);
            Assert.AreEqual("рублів", declensionResult.UnitDeclension.Genitive);
            Assert.AreEqual("рублям", declensionResult.UnitDeclension.Dative);
            Assert.AreEqual("рублів", declensionResult.UnitDeclension.Accusative);
            Assert.AreEqual("рублями", declensionResult.UnitDeclension.Instrumental);
            Assert.AreEqual("рублях", declensionResult.UnitDeclension.Prepositional);
            Assert.AreEqual("рублів", declensionResult.UnitDeclension.Vocative);
        }


        [Test]
        public void Parse_MorpherWebServiceException()
        {
            Mock<IWebClient> webClient = new Mock<IWebClient>();
            webClient.Setup(client => client.QueryString).Returns(new NameValueCollection());
            WebException exception = new WebException("Exception", null, WebExceptionStatus.ReceiveFailure,
                WebResponseMock.CreateWebResponse((HttpStatusCode)495,
                    new MemoryStream(Encoding.UTF8.GetBytes(ExceptionText.UseSpell))));
            webClient.Setup(client => client.DownloadString(It.IsAny<string>())).Throws(exception);
            MorpherClient morpherClient = new MorpherClient();
            morpherClient.NewClient = () => new MyWebClient(morpherClient.Token, morpherClient.Url)
            {
                WebClient = webClient.Object
            };

            Assert.Throws<NumeralsDeclensionNotSupportedException>(() => morpherClient.Ukrainian.Parse("exception here"));
        }

        [Test]
        public void Spell_MorpherWebServiceException()
        {
            Mock<IWebClient> webClient = new Mock<IWebClient>();
            webClient.Setup(client => client.QueryString).Returns(new NameValueCollection());
            WebException exception = new WebException("Exception", null, WebExceptionStatus.ReceiveFailure,
                WebResponseMock.CreateWebResponse((HttpStatusCode)400,
                    new MemoryStream(Encoding.UTF8.GetBytes(ExceptionText.MissedParameter))));
            webClient.Setup(client => client.DownloadString(It.IsAny<string>())).Throws(exception);
            MorpherClient morpherClient = new MorpherClient();
            morpherClient.NewClient = () => new MyWebClient(morpherClient.Token, morpherClient.Url)
            {
                WebClient = webClient.Object
            };

            Assert.Throws<RequiredParameterIsNotSpecifiedException>(() => morpherClient.Ukrainian.Parse("exception here"));
        }
    }
}