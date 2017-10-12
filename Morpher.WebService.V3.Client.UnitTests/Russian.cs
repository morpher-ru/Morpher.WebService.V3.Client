namespace Morpher.WebService.V3.Client.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using Moq;
    using NUnit.Framework;
    using V3.Russian;

    [TestFixture]
    public class Russian
    {
        public string DeclensionResultText { get; } = @"
{
    ""Р"": ""теста"",
    ""Д"": ""тесту"",
    ""В"": ""тест"",
    ""Т"": ""тестом"",
    ""П"": ""тесте"",
    ""П_о"": ""о тесте"",
    ""род"": ""Мужской"",
    ""множественное"": {
        ""И"": ""тесты"",
        ""Р"": ""тестов"",
        ""Д"": ""тестам"",
        ""В"": ""тесты"",
        ""Т"": ""тестами"",
        ""П"": ""тестах"",
        ""П_о"": ""о тестах""
    },
    ""где"": ""в тесте"",
    ""куда"": ""в тест"",
    ""откуда"": ""из теста""
}";

        public string SpellResultText { get; } = @"
{
    ""n"": {
        ""И"": ""десять"",
        ""Р"": ""десяти"",
        ""Д"": ""десяти"",
        ""В"": ""десять"",
        ""Т"": ""десятью"",
        ""П"": ""десяти""
    },
    ""unit"": {
        ""И"": ""рублей"",
        ""Р"": ""рублей"",
        ""Д"": ""рублям"",
        ""В"": ""рублей"",
        ""Т"": ""рублями"",
        ""П"": ""рублях""
    }
}";

        public string FioSplit { get; } = @"
{
  ""Р"": ""Александра Пушкина"",
  ""Д"": ""Александру Пушкину"",
  ""В"": ""Александра Пушкина"",
  ""Т"": ""Александром Пушкиным"",
  ""П"": ""Александре Пушкине"",
  ""ФИО"": {
    ""Ф"": ""Пушкин"",
    ""И"": ""Александр"",
    ""О"": ""Сергеевич""
  }
}";

        public string GendersResultText { get; } = @"
{
  ""feminine"": ""уважаемая"",
  ""neuter"": ""уважаемое"",
  ""plural"": ""уважаемые""
}";

        public string AdjectivizeResultText { get; } = @"
[
  ""мытищинский"",
  ""мытищенский""
]";

        public string UserDictGetAllText { get; } = @"
[
    {
        ""singular"": {
            ""И"": ""Кошка"",
            ""Р"": ""Пантеры"",
            ""Д"": ""Пантере"",
            ""В"": ""Пантеру"",
            ""Т"": ""Пантерой"",
            ""П"": ""о Пантере"",
            ""М"": ""в Пантере""
        },
        ""plural"": {
            ""И"": ""Пантеры"",
            ""Р"": ""Пантер"",
            ""Д"": ""Пантерам"",
            ""В"": ""Пантер"",
            ""Т"": ""Пантерами"",
            ""П"": ""о Пантерах"",
            ""М"": ""в Пантерах""
        }
    }
]";


        

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

            DeclensionResult declensionResult = morpherClient.Russian.Parse("тест");
            Assert.IsNotNull(declensionResult);
            Assert.IsNotNull(declensionResult.Plural);
            Assert.AreEqual("тест", declensionResult.Nominative);
            Assert.AreEqual("теста", declensionResult.Genitive);
            Assert.AreEqual("тесту", declensionResult.Dative);
            Assert.AreEqual("тест", declensionResult.Accusative);
            Assert.AreEqual("тестом", declensionResult.Instrumental);
            Assert.AreEqual("тесте", declensionResult.Prepositional);
            Assert.AreEqual("о тесте", declensionResult.PrepositionalWithO);

            Assert.AreEqual("в тесте", declensionResult.Where);
            Assert.AreEqual("в тест", declensionResult.To);
            Assert.AreEqual("из теста", declensionResult.From);

            Assert.AreEqual("тесты", declensionResult.Plural.Nominative);
            Assert.AreEqual("тестов", declensionResult.Plural.Genitive);
            Assert.AreEqual("тестам", declensionResult.Plural.Dative);
            Assert.AreEqual("тесты", declensionResult.Plural.Accusative);
            Assert.AreEqual("тестами", declensionResult.Plural.Instrumental);
            Assert.AreEqual("тестах", declensionResult.Plural.Prepositional);
            Assert.AreEqual("о тестах", declensionResult.Plural.PrepositionalWithO);

            Assert.AreEqual(Gender.Masculine, declensionResult.Gender);
        }

        [Test]
        public void SplitFio_Success()
        {
            Mock<IWebClient> webClient = new Mock<IWebClient>();
            webClient.Setup(client => client.QueryString).Returns(new NameValueCollection());
            webClient.Setup(client => client.DownloadString(It.IsAny<string>())).Returns(FioSplit);
            MorpherClient morpherClient = new MorpherClient();
            morpherClient.NewClient = () => new MyWebClient(morpherClient.Token, morpherClient.Url)
            {
                WebClient = webClient.Object
            };

            DeclensionResult declensionResult = morpherClient.Russian.Parse("Александр Пушкин Сергеевич");
            Assert.IsNotNull(declensionResult);
            Assert.IsNotNull(declensionResult.FullName);
            Assert.AreEqual("Пушкин", declensionResult.FullName.Surname);
            Assert.AreEqual("Александр", declensionResult.FullName.Name);
            Assert.AreEqual("Сергеевич", declensionResult.FullName.Pantronymic);
        }

        [Test]
        public void Spell_Success()
        {
            Mock<IWebClient> webClient = new Mock<IWebClient>();
            webClient.Setup(client => client.QueryString).Returns(new NameValueCollection());
            webClient.Setup(client => client.DownloadString(It.IsAny<string>())).Returns(SpellResultText);
            MorpherClient morpherClient = new MorpherClient();
            morpherClient.NewClient = () => new MyWebClient(morpherClient.Token, morpherClient.Url)
            {
                WebClient = webClient.Object
            };

            NumberSpellingResult declensionResult = morpherClient.Russian.Spell(10, "рубль");
            Assert.IsNotNull(declensionResult);

            // number
            Assert.AreEqual("десять", declensionResult.NumberDeclension.Nominative);
            Assert.AreEqual("десяти", declensionResult.NumberDeclension.Genitive);
            Assert.AreEqual("десяти", declensionResult.NumberDeclension.Dative);
            Assert.AreEqual("десять", declensionResult.NumberDeclension.Accusative);
            Assert.AreEqual("десятью", declensionResult.NumberDeclension.Instrumental);
            Assert.AreEqual("десяти", declensionResult.NumberDeclension.Prepositional);


            // unit
            Assert.AreEqual("рублей", declensionResult.UnitDeclension.Nominative);
            Assert.AreEqual("рублей", declensionResult.UnitDeclension.Genitive);
            Assert.AreEqual("рублям", declensionResult.UnitDeclension.Dative);
            Assert.AreEqual("рублей", declensionResult.UnitDeclension.Accusative);
            Assert.AreEqual("рублями", declensionResult.UnitDeclension.Instrumental);
            Assert.AreEqual("рублях", declensionResult.UnitDeclension.Prepositional);
        }

        [Test]
        public void Genders_Success()
        {
            Mock<IWebClient> webClient = new Mock<IWebClient>();
            webClient.Setup(client => client.QueryString).Returns(new NameValueCollection());
            webClient.Setup(client => client.DownloadString(It.IsAny<string>())).Returns(GendersResultText);
            MorpherClient morpherClient = new MorpherClient();
            morpherClient.NewClient = () => new MyWebClient(morpherClient.Token, morpherClient.Url)
            {
                WebClient = webClient.Object
            };

            AdjectiveGenders adjectiveGenders = morpherClient.Russian.AdjectiveGenders("уважаемый");
            Assert.IsNotNull(adjectiveGenders);

            Assert.AreEqual("уважаемая", adjectiveGenders.Feminie);
            Assert.AreEqual("уважаемое", adjectiveGenders.Neuter);
            Assert.AreEqual("уважаемые", adjectiveGenders.Plural);
        }

        [Test]
        public void Adjectivize_Success()
        {

            Mock<IWebClient> webClient = new Mock<IWebClient>();
            webClient.Setup(client => client.QueryString).Returns(new NameValueCollection());
            webClient.Setup(client => client.DownloadString(It.IsAny<string>())).Returns(AdjectivizeResultText);
            MorpherClient morpherClient = new MorpherClient();
            morpherClient.NewClient = () => new MyWebClient(morpherClient.Token, morpherClient.Url)
            {
                WebClient = webClient.Object
            };

            List<string> adjList = morpherClient.Russian.Adjectivize("мытыщи");
            Assert.IsNotNull(adjList);
            Assert.AreEqual("мытищинский", adjList[0]);
            Assert.AreEqual("мытищенский", adjList[1]);
        }

        [Test]
        public void Parse_Exception()
        {
            Assert.Throws<ArgumentEmptyException>(() => 
            MockClientHelpers.ExceptionClient().Russian.Parse("exception here"));
        }

        [Test]
        public void Spell_Exception()
        {
            Assert.Throws<ArgumentEmptyException>(() => 
            MockClientHelpers.ExceptionClient().Russian.Spell(1, "exception here"));
        }

        [Test]
        public void Genders_Exception()
        {
            Assert.Throws<ArgumentEmptyException>(() => 
            MockClientHelpers.ExceptionClient().Russian.AdjectiveGenders("exception here"));
        }

        [Test]
        public void Adjectivize_Exception()
        {
            Assert.Throws<ArgumentEmptyException>(() => 
            MockClientHelpers.ExceptionClient().Russian.Adjectivize("exception here"));
        }

        [Test]
        public void UserDictRemove_Success()
        {
            NameValueCollection @params = new NameValueCollection();
            Mock<IWebClient> webClient = new Mock<IWebClient>();
            webClient.Setup(client => client.QueryString).Returns(@params);
            webClient.Setup(client => client.UploadValues(It.IsAny<string>(), "DELETE", It.IsAny<NameValueCollection>()))
                .Returns(Encoding.UTF8.GetBytes("true"));
            MorpherClient morpherClient = new MorpherClient();
            morpherClient.NewClient = () => new MyWebClient(morpherClient.Token, morpherClient.Url)
            {
                WebClient = webClient.Object
            };

            bool found = morpherClient.Russian.UserDict.Remove("кошка");

            Assert.IsTrue(found);
            Assert.AreEqual("кошка", @params.Get("s"));
        }

        [Test]
        public void UserDictRemove_Exception()
        {
            NameValueCollection @params = new NameValueCollection();
            Mock<IWebClient> webClient = new Mock<IWebClient>();
            webClient.Setup(client => client.QueryString).Returns(@params);
            WebException exception = new WebException("Exception", null, WebExceptionStatus.ReceiveFailure,
                WebResponseMock.CreateWebResponse((HttpStatusCode)400,
                    new MemoryStream(Encoding.UTF8.GetBytes(ExceptionText.MissedParameter))));
            webClient.Setup(
                    client => client.UploadValues(It.IsAny<string>(), "DELETE", It.IsAny<NameValueCollection>()))
                .Throws(exception);
            MorpherClient morpherClient = new MorpherClient();
            morpherClient.NewClient = () => new MyWebClient(morpherClient.Token, morpherClient.Url)
            {
                WebClient = webClient.Object
            };

            Assert.Throws<ArgumentEmptyException>(() => morpherClient.Russian.UserDict.Remove("exception here"));
        }

        [Test]
        public void UserDictGetAll_Success()
        {
            Mock<IWebClient> webClient = new Mock<IWebClient>();
            webClient.Setup(client => client.QueryString).Returns(new NameValueCollection());
            webClient.Setup(client => client.DownloadString(It.IsAny<string>())).Returns(UserDictGetAllText);
            MorpherClient morpherClient = new MorpherClient();
            morpherClient.NewClient = () => new MyWebClient(morpherClient.Token, morpherClient.Url)
            {
                WebClient = webClient.Object
            };

            IEnumerable<CorrectionEntry> correctionEntries = morpherClient.Russian.UserDict.GetAll()?.ToList();

            Assert.IsNotNull(correctionEntries);
            Assert.AreEqual(1, correctionEntries.Count());
            CorrectionEntry entry = correctionEntries.First();

            Assert.AreEqual("Кошка", entry.Singular.Nominative);
            Assert.AreEqual("Пантеры", entry.Singular.Genitive);
            Assert.AreEqual("Пантере", entry.Singular.Dative);
            Assert.AreEqual("Пантеру", entry.Singular.Accusative);
            Assert.AreEqual("Пантерой", entry.Singular.Instrumental);
            Assert.AreEqual("о Пантере", entry.Singular.Prepositional);
            Assert.AreEqual("в Пантере", entry.Singular.Locative);

            Assert.AreEqual("Пантеры", entry.Plural.Nominative);
            Assert.AreEqual("Пантер", entry.Plural.Genitive);
            Assert.AreEqual("Пантерам", entry.Plural.Dative);
            Assert.AreEqual("Пантер", entry.Plural.Accusative);
            Assert.AreEqual("Пантерами", entry.Plural.Instrumental);
            Assert.AreEqual("о Пантерах", entry.Plural.Prepositional);
            Assert.AreEqual("в Пантерах", entry.Plural.Locative);
        }

        [Test]
        public void UserDictGetAll_Exception()
        {
            Assert.Throws<ArgumentEmptyException>(() => 
            MockClientHelpers.ExceptionClient().Russian.UserDict.GetAll());
        }

        [Test]
        public void InternalServerError()
        {
            Assert.Throws<WebException>(() => 
            MockClientHelpers.ExceptionClient(ExceptionText.ServerError, HttpStatusCode.InternalServerError).Russian.UserDict.GetAll());
        }

        /// <summary>
        /// Сервис не должен маскировать исключения, которые выбрасывает IWebClient (кроме WebException).
        /// </summary>
        [Test]
        public void ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => 
            MockClientHelpers.ExceptionClient(new ArgumentNullException()).Russian.UserDict.GetAll());
        }
    }
}